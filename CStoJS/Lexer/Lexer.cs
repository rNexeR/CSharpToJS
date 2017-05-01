using System;
using System.Collections.Generic;
using System.Text;

namespace CStoJS
{
    public class Lexer
    {
        private InputString inputString;
        private Symbol currentSymbol;
        private Dictionary<string, TokenType> reservedWordsDict;
        private Dictionary<char, TokenType> oneSymbolDict;
        private Dictionary<string, TokenType> multipleOptionsDict;
        private List<string> escapeSequences;

        public Lexer(InputString inputString)
        {
            this.inputString = inputString;
            this.currentSymbol = inputString.GetNextSymbol();
            InitReservedWordsDictionary();
            InitOneSymbolDictionary();
            InitMultipleOptionsDictionary();
            InitEscapeSequences();
        }

        private void InitEscapeSequences()
        {
            escapeSequences = new List<string>();
            escapeSequences.Add("\t");
            escapeSequences.Add("\n");
            escapeSequences.Add("\'");
            escapeSequences.Add("\"");
            escapeSequences.Add("\\");
            escapeSequences.Add("\0");
            escapeSequences.Add("\a");
            escapeSequences.Add("\b");
            escapeSequences.Add("\f");
            escapeSequences.Add("\r");
            escapeSequences.Add("\v");
        }

        private void InitMultipleOptionsDictionary()
        {
            multipleOptionsDict = new Dictionary<string, TokenType>();
            multipleOptionsDict["+"] = TokenType.OP_SUM;
            multipleOptionsDict["++"] = TokenType.OP_INC_PP;
            multipleOptionsDict["+="] = TokenType.OP_INC_PE;
            multipleOptionsDict["-"] = TokenType.OP_SUBSTRACT;
            multipleOptionsDict["--"] = TokenType.OP_INC_MM;
            multipleOptionsDict["="] = TokenType.OP_ASSIGN;
            multipleOptionsDict["=="] = TokenType.OP_CONDITIONAL_EQUAL;
            multipleOptionsDict["?"] = TokenType.OP_CONDITIONAL;
            multipleOptionsDict["??"] = TokenType.OP_NULL_COALESCING;
            multipleOptionsDict[">"] = TokenType.OP_GREATER_THAN;
            multipleOptionsDict[">="] = TokenType.OP_GREATER_EQUAL_THAN;
            multipleOptionsDict[">>"] = TokenType.OP_BITS_SHIFT_RIGHT;
            multipleOptionsDict["<"] = TokenType.OP_LESS_THAN;
            multipleOptionsDict["<="] = TokenType.OP_LESS_EQUAL_THAN;
            multipleOptionsDict["<<"] = TokenType.OP_BITS_SHIFT_LEFT;
            multipleOptionsDict["&"] = TokenType.OP_BITS_AND;
            multipleOptionsDict["&&"] = TokenType.OP_CONDITIONAL_AND;
            multipleOptionsDict["|"] = TokenType.OP_BITS_OR;
            multipleOptionsDict["||"] = TokenType.OP_CONDITIONAL_OR;
            multipleOptionsDict["!"] = TokenType.OP_NEGATION;
            multipleOptionsDict["!="] = TokenType.OP_CONDITIONAL_NOT_EQUAL;
        }

        private void InitOneSymbolDictionary()
        {
            oneSymbolDict = new Dictionary<char, TokenType>();
            oneSymbolDict['('] = TokenType.PAREN_OPEN;
            oneSymbolDict[')'] = TokenType.PAREN_CLOSE;
            oneSymbolDict['{'] = TokenType.BRACE_OPEN;
            oneSymbolDict['}'] = TokenType.BRACE_CLOSE;
            oneSymbolDict[']'] = TokenType.BRACKET_CLOSE;
            oneSymbolDict['['] = TokenType.BRACKET_OPEN;
            oneSymbolDict['*'] = TokenType.OP_MULTIPLICATION;
            oneSymbolDict['/'] = TokenType.OP_DIVISION;
            oneSymbolDict['~'] = TokenType.OP_BITS_COMPLEMENT;
            oneSymbolDict[';'] = TokenType.END_STATEMENT;
            oneSymbolDict[':'] = TokenType.OP_HIERARCHY;
            oneSymbolDict['.'] = TokenType.OP_MEMBER_ACCESS;
            oneSymbolDict['%'] = TokenType.OP_MODULO;
            oneSymbolDict['^'] = TokenType.OP_BITS_XOR;
            oneSymbolDict['\0'] = TokenType.EOF;
        }

        private void InitReservedWordsDictionary()
        {
            reservedWordsDict = new Dictionary<string, TokenType>();
            reservedWordsDict["int"] = TokenType.INT_TYPE;
            reservedWordsDict["float"] = TokenType.FLOAT_TYPE;
            reservedWordsDict["bool"] = TokenType.BOOL_TYPE;
            reservedWordsDict["string"] = TokenType.STRING_TYPE;
            reservedWordsDict["char"] = TokenType.CHAR_TYPE;
            reservedWordsDict["true"] = TokenType.TRUE_BOOL_VALUE;
            reservedWordsDict["false"] = TokenType.FALSE_BOOL_VALUE;
            reservedWordsDict["as"] = TokenType.OP_AS;
            reservedWordsDict["is"] = TokenType.OP_IS;
        }

        public Token GetNextToken()
        {
            while (Char.IsWhiteSpace(currentSymbol.character))
                currentSymbol = inputString.GetNextSymbol();

            if (oneSymbolDict.ContainsKey(currentSymbol.character))
            {
                var ret = new Token(oneSymbolDict[currentSymbol.character], currentSymbol.character.ToString(), currentSymbol.rowCount, currentSymbol.colCount);
                currentSymbol = inputString.GetNextSymbol();
                return ret;
            }
            else if (multipleOptionsDict.ContainsKey(currentSymbol.character.ToString()))
                return MultipleOptionsSelector();
            else if(currentSymbol.character == '\'')
                return CharLiteralDetector();
            else if(currentSymbol.character == '"')
                return StringLiteralDetector();
            else if(currentSymbol.character == '@')
                return StringVerbatimLiteralDetector();
            else if (Char.IsDigit(currentSymbol.character))
                return DigitOptionsSelector();
            else if (Char.IsLetter(currentSymbol.character) || currentSymbol.character == '_')
                return LetterOptionsSelector();
            else
                throw new LexicalException("Symbol not supported.");
        }

        private Token MultipleOptionsSelector()
        {
            var lexema = new StringBuilder();
            lexema.Append(currentSymbol.character);

            var ret = new Token(multipleOptionsDict[lexema.ToString()], lexema.ToString(), currentSymbol.rowCount, currentSymbol.colCount);

            currentSymbol = inputString.GetNextSymbol();
            lexema.Append(currentSymbol.character);

            if (multipleOptionsDict.ContainsKey(lexema.ToString()))
            {
                ret.type = multipleOptionsDict[lexema.ToString()];
                ret.lexema = lexema.ToString();
                currentSymbol = inputString.GetNextSymbol();
            }

            return ret;
        }

        private Token CharLiteralDetector()
        {
            var lexema = new StringBuilder(currentSymbol.character.ToString());
            var lex = lexema.ToString();
            var ret = new Token(TokenType.LITERAL_CHAR, lexema.ToString(), currentSymbol.rowCount, currentSymbol.colCount);

            do
            {
                currentSymbol = inputString.GetNextSymbol();
                if(currentSymbol.character == '\n' || currentSymbol.character == '\0')
                    throw new CharLiteralException("Char Literal must be closed.");
                lexema.Append(currentSymbol.character);
                lex = lexema.ToString();
            } while (currentSymbol.character != '\'');

            ret.lexema = lexema.ToString();
            currentSymbol = inputString.GetNextSymbol();

            if(ret.lexema.Length > 3)
                throw new CharLiteralException("Too many characters in Char Literal.");
            return ret;
        }

        private Token StringLiteralDetector()
        {
            var lexema = new StringBuilder(currentSymbol.character.ToString());
            var lex = lexema.ToString();
            var ret = new Token(TokenType.LITERAL_STRING, lexema.ToString(), currentSymbol.rowCount, currentSymbol.colCount);

            do
            {
                currentSymbol = inputString.GetNextSymbol();
                if(currentSymbol.character == '\n' || currentSymbol.character == '\0')
                    throw new StringLiteralException("String Literal must be closed.");
                lexema.Append(currentSymbol.character);
                lex = lexema.ToString();
            } while (currentSymbol.character != '"');

            ret.lexema = lexema.ToString();
            currentSymbol = inputString.GetNextSymbol();

            return ret;
        }

        private Token StringVerbatimLiteralDetector()
        {
            var ret = new Token(TokenType.LITERAL_STRING_VERBATIM, "", currentSymbol.rowCount, currentSymbol.colCount);
            currentSymbol = inputString.GetNextSymbol();
            var str = StringLiteralDetector();

            ret.lexema = str.lexema;

            return ret;
        }

        private Token LetterOptionsSelector()
        {
            var lexema = new StringBuilder();
            var ret = new Token(TokenType.ID, lexema.ToString(), currentSymbol.rowCount, currentSymbol.colCount);

            do
            {
                lexema.Append(currentSymbol.character);
                currentSymbol = inputString.GetNextSymbol();
            } while (Char.IsLetter(currentSymbol.character) || Char.IsDigit(currentSymbol.character) || currentSymbol.character == '_');

            ret.type = reservedWordsDict.ContainsKey(lexema.ToString()) ?
                reservedWordsDict[lexema.ToString()] : TokenType.ID;

            ret.lexema = lexema.ToString();

            return ret;
        }

        private Token DigitOptionsSelector()
        {
            var lexema = new StringBuilder(currentSymbol.character.ToString());
            var ret = new Token(TokenType.LITERAL_INT, lexema.ToString(), currentSymbol.rowCount, currentSymbol.colCount);
            currentSymbol = inputString.GetNextSymbol();

            while(Char.IsDigit(currentSymbol.character) || (currentSymbol.character == '.' && !lexema.ToString().Contains("."))){
                lexema.Append(currentSymbol.character);
                if(currentSymbol.character == '.')
                    ret.type = TokenType.LITERAL_FLOAT;
                currentSymbol = inputString.GetNextSymbol();
            }

            if(ret.type == TokenType.LITERAL_FLOAT){
                if(currentSymbol.character != 'F')
                    throw new FloatLiteralException("Float Literal must finish with F.");
                else
                    lexema.Append(currentSymbol.character);
                    currentSymbol = inputString.GetNextSymbol();
            }

            ret.lexema = lexema.ToString();

            return ret;
        }
    }
}
