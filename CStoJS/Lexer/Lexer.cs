using CStoJS.Inputs;
using System.Collections.Generic;
using CStoJS.Exceptions;
using System;
using System.Text;

namespace CStoJS.LexerLibraries
{
    public class Lexer
    {
        private InputString inputString;
        private Symbol currentSymbol;
        private Dictionary<string, TokenType> reservedWordsDict;
        private Dictionary<char, TokenType> oneSymbolDict;
        private Dictionary<string, TokenType> multipleOptionsDict;
        private List<string> escapeSequences;
        private int max_multiple_options_chars;

        public Lexer(InputString inputString)
        {
            this.inputString = inputString;
            this.currentSymbol = inputString.GetNextSymbol();
            max_multiple_options_chars = 3;
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
            multipleOptionsDict["+="] = TokenType.OP_ASSIGN_PLUS;
            multipleOptionsDict["-"] = TokenType.OP_SUBSTRACT;
            multipleOptionsDict["--"] = TokenType.OP_INC_MM;
            multipleOptionsDict["-="] = TokenType.OP_ASSIGN_MINUS;
            multipleOptionsDict["="] = TokenType.OP_ASSIGN;
            multipleOptionsDict["=="] = TokenType.OP_CONDITIONAL_EQUAL;
            multipleOptionsDict["?"] = TokenType.OP_TERNARY;
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

            multipleOptionsDict["//"] = TokenType.LINE_COMMENT;
            multipleOptionsDict["/*"] = TokenType.BLOCK_COMMENT;

            multipleOptionsDict["%"] = TokenType.OP_MODULO;
            multipleOptionsDict["%="] = TokenType.OP_ASSIGN_MODULO;
            multipleOptionsDict["*"] = TokenType.OP_MULTIPLICATION;
            multipleOptionsDict["*="] = TokenType.OP_ASSIGN_MULTIPLICATION;
            multipleOptionsDict["/"] = TokenType.OP_DIVISION;
            multipleOptionsDict["/="] = TokenType.OP_ASSIGN_DIVISION;
            multipleOptionsDict["&="] = TokenType.OP_ASSIGN_AND;
            multipleOptionsDict["|="] = TokenType.OP_ASSIGN_OR;
            multipleOptionsDict["^"] = TokenType.OP_BITS_XOR;
            multipleOptionsDict["^="] = TokenType.OP_ASSIGN_XOR;
            multipleOptionsDict["<<="] = TokenType.OP_ASSIGN_SHIFT_LEFT;
            multipleOptionsDict[">>="] = TokenType.OP_ASSIGN_SHIFT_RIGHT;
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
            oneSymbolDict['~'] = TokenType.OP_BITS_COMPLEMENT;
            oneSymbolDict[';'] = TokenType.END_STATEMENT;
            oneSymbolDict[':'] = TokenType.OP_HIERARCHY;
            oneSymbolDict['.'] = TokenType.OP_MEMBER_ACCESS;
            oneSymbolDict[','] = TokenType.COMMA;
            oneSymbolDict['~'] = TokenType.OP_BITS_COMPLEMENT;
            oneSymbolDict['\0'] = TokenType.EOF;
        }

        private void InitReservedWordsDictionary()
        {
            reservedWordsDict = new Dictionary<string, TokenType>();

            reservedWordsDict["public"] = TokenType.PUBLIC_KEYWORD;
            reservedWordsDict["private"] = TokenType.PRIVATE_KEYWORD;
            reservedWordsDict["protected"] = TokenType.PROTECTED_KEYWORD;
            reservedWordsDict["static"] = TokenType.STATIC_KEYWORD;
            reservedWordsDict["abstract"] = TokenType.ABSTRACT_KEYWORD;
            reservedWordsDict["virtual"] = TokenType.VIRTUAL_KEYWORD;
            reservedWordsDict["override"] = TokenType.OVERRIDE_KEYWORD;

            reservedWordsDict["class"] = TokenType.CLASS_KEYWORD;
            reservedWordsDict["enum"] = TokenType.ENUM_KEYWORD;
            reservedWordsDict["interface"] = TokenType.INTERFACE_KEYWORD;

            reservedWordsDict["namespace"] = TokenType.NAMESPACE_KEYWORD;
            reservedWordsDict["base"] = TokenType.BASE_KEYWORD;

            reservedWordsDict["if"] = TokenType.IF_KEYWORD;
            reservedWordsDict["else"] = TokenType.ELSE_KEYWORD;
            reservedWordsDict["switch"] = TokenType.SWITCH_KEYWORD;
            reservedWordsDict["case"] = TokenType.CASE_KEYWORD;
            reservedWordsDict["default"] = TokenType.DEFAULT_KEYWORD;

            reservedWordsDict["while"] = TokenType.WHILE_KEYWORD;
            reservedWordsDict["for"] = TokenType.FOR_KEYWORD;
            reservedWordsDict["foreach"] = TokenType.FOREACH_KEYWORD;
            reservedWordsDict["do"] = TokenType.DO_KEYWORD;

            reservedWordsDict["int"] = TokenType.INT_KEYWORD;
            reservedWordsDict["bool"] = TokenType.BOOL_KEYWORD;
            reservedWordsDict["char"] = TokenType.CHAR_KEYWORD;
            reservedWordsDict["float"] = TokenType.FLOAT_KEYWORD;
            reservedWordsDict["string"] = TokenType.STRING_KEYWORD;
            reservedWordsDict["void"] = TokenType.VOID_KEYWORD;
            reservedWordsDict["var"] = TokenType.VAR_KEYWORD;

            reservedWordsDict["break"] = TokenType.BREAK_KEYWORD;
            reservedWordsDict["continue"] = TokenType.CONTINUE_KEYWORD;
            reservedWordsDict["return"] = TokenType.RETURN_KEYWORD;

            reservedWordsDict["is"] = TokenType.IS_KEYWORD;
            reservedWordsDict["as"] = TokenType.AS_KEYWORD;
            reservedWordsDict["in"] = TokenType.IN_KEYWORD;

            reservedWordsDict["new"] = TokenType.NEW_KEYWORD;
            reservedWordsDict["this"] = TokenType.THIS_KEYWORD;

            reservedWordsDict["true"] = TokenType.TRUE_KEYWORD;
            reservedWordsDict["false"] = TokenType.FALSE_KEYWORD;

            reservedWordsDict["using"] = TokenType.USING_KEYWORD;

            reservedWordsDict["null"] = TokenType.NULL_KEYWORD;
        }

        private void ThrowException(string msg){
            throw new LexicalException($"{msg} [{currentSymbol.character}] at [{currentSymbol.rowCount}, {currentSymbol.colCount}]");
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
            else{
                ThrowException("Symbol not supported.");
                return new Token(TokenType.EOF, "", 0, 0);
            }
        }

        private Token MultipleOptionsSelector()
        {
            var lexema = new StringBuilder();
            lexema.Append(currentSymbol.character);

            var ret = new Token(multipleOptionsDict[lexema.ToString()], lexema.ToString(), currentSymbol.rowCount, currentSymbol.colCount);

            currentSymbol = inputString.GetNextSymbol();
            lexema.Append(currentSymbol.character);

            int i = 0;
            while (i < max_multiple_options_chars && multipleOptionsDict.ContainsKey(lexema.ToString()))
            {
                ret.type = multipleOptionsDict[lexema.ToString()];
                ret.lexema = lexema.ToString();
                currentSymbol = inputString.GetNextSymbol();
                lexema.Append(currentSymbol.character);

                if(ret.type == TokenType.LINE_COMMENT){
                    while(currentSymbol.character != '\n'){
                        currentSymbol = inputString.GetNextSymbol();
                        if(currentSymbol.character == '\0'){
                            ThrowException("Block Comment must be closed");
                        }
                    }
                    return GetNextToken();
                }
                else if(ret.type == TokenType.BLOCK_COMMENT){
                    while(true){
                        currentSymbol = inputString.GetNextSymbol();
                        if(currentSymbol.character == '*'){
                            currentSymbol = inputString.GetNextSymbol();
                            if(currentSymbol.character == '/'){
                                currentSymbol = inputString.GetNextSymbol();
                                return GetNextToken();
                            }else if(currentSymbol.character == '\0'){
                                ThrowException("Block Comment must be closed");
                            }
                        }else if(currentSymbol.character == '\0'){
                            ThrowException("Block Comment must be closed");
                        }
                    }
                }
                i++;
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
                    ThrowException("Char Literal must be closed.");
                lexema.Append(currentSymbol.character);
                lex = lexema.ToString();
            } while (currentSymbol.character != '\'');

            ret.lexema = lexema.ToString();
            currentSymbol = inputString.GetNextSymbol();

            if(ret.lexema.Length > 3)
                ThrowException("Too many characters in Char Literal.");
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
                    ThrowException("String Literal must be closed.");
                lexema.Append(currentSymbol.character);
                lex = lexema.ToString();
            } while (currentSymbol.character != '"');

            ret.lexema = lexema.ToString();
            currentSymbol = inputString.GetNextSymbol();

            return ret;
        }

        private Token StringVerbatimLiteralDetector()
        {
            var lexema = new StringBuilder(currentSymbol.character.ToString());
            var lex = lexema.ToString();
            var ret = new Token(TokenType.LITERAL_STRING_VERBATIM, lexema.ToString(), currentSymbol.rowCount, currentSymbol.colCount);
            currentSymbol = inputString.GetNextSymbol();
            lexema.Append('"');

            do
            {
                currentSymbol = inputString.GetNextSymbol();
                if(currentSymbol.character == '"'){
                    lexema.Append(currentSymbol.character);
                    currentSymbol = inputString.GetNextSymbol();
                    if(currentSymbol.character == '"'){
                        lexema.Append(currentSymbol.character);
                        currentSymbol = inputString.GetNextSymbol();
                        //continue;
                    }else{
                        break;
                    }
                }
                    
                lexema.Append(currentSymbol.character);
            } while (currentSymbol.character != '"');

            ret.lexema = lexema.ToString();
            currentSymbol = inputString.GetNextSymbol();


            // var str = StringLiteralDetector();

            // ret.lexema = "@" + str.lexema;

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
                if(currentSymbol.character != 'F' && currentSymbol.character != 'f')
                    ThrowException("Float Literal must finish with F.");
                else
                    lexema.Append(currentSymbol.character);
                    currentSymbol = inputString.GetNextSymbol();
            }

            ret.lexema = lexema.ToString();

            return ret;
        }
    }
}

