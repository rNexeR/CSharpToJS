using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;
using System.Linq;
using System;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private Lexer lexer;
        private Token currentToken;
        private TokenType[] encapsulation_modifiers, class_modifiers, optional_modifiers;
        private TokenType[] types, literals, unary_operators, assignment_operators;
        private TokenType[] relational_operators, equality_operators, shift_operators;
        private TokenType[] multiplicative_operators, is_as_operators, expression_operators;
        private bool enableDebug = false;

        public Parser(Lexer lexer){
            this.lexer = lexer;
            this.InitializeArrays();
            enableDebug = true;
        }

        public void InitializeArrays(){
            this.encapsulation_modifiers = new TokenType[] { TokenType.PRIVATE_KEYWORD, TokenType.PROTECTED_KEYWORD, TokenType.PUBLIC_KEYWORD };
            this.class_modifiers = new TokenType[] { TokenType.ABSTRACT_KEYWORD };
            this.types = new TokenType[]{ TokenType.INT_KEYWORD, TokenType.FLOAT_KEYWORD, TokenType.CHAR_KEYWORD, TokenType.STRING_KEYWORD, TokenType.BOOL_KEYWORD, TokenType.ID };
            this.optional_modifiers = new TokenType[]{ TokenType.STATIC_KEYWORD, TokenType.VIRTUAL_KEYWORD, TokenType.OVERRIDE_KEYWORD, TokenType.ABSTRACT_KEYWORD };
            this.literals = new TokenType[]{ TokenType.LITERAL_INT, TokenType.LITERAL_CHAR, TokenType.LITERAL_FLOAT, TokenType.LITERAL_STRING, TokenType.LITERAL_STRING_VERBATIM };
            this.is_as_operators = new TokenType[]{ TokenType.AS_KEYWORD, TokenType.IS_KEYWORD };
            this.multiplicative_operators = new TokenType[]{ TokenType.OP_MULTIPLICATION, TokenType.OP_DIVISION, TokenType.OP_MODULO };
            this.shift_operators = new TokenType[]{ TokenType.OP_BITS_SHIFT_LEFT, TokenType.OP_BITS_SHIFT_RIGHT };
            this.equality_operators = new TokenType[]{ TokenType.OP_CONDITIONAL_EQUAL, TokenType.OP_CONDITIONAL_NOT_EQUAL };
            this.relational_operators = new TokenType[]{ TokenType.OP_LESS_THAN, TokenType.OP_GREATER_THAN, TokenType.OP_LESS_EQUAL_THAN, TokenType.OP_GREATER_EQUAL_THAN };
            this.assignment_operators = new TokenType[]{ TokenType.OP_ASSIGN, TokenType.OP_ASSIGN_PLUS, TokenType.OP_ASSIGN_MINUS, TokenType.OP_ASSIGN_MULTIPLICATION, TokenType.OP_ASSIGN_DIVISION, TokenType.OP_ASSIGN_MODULO, TokenType.OP_ASSIGN__AND, TokenType.OP_ASSIGN__OR, TokenType.OP_ASSIGN_XOR, TokenType.OP_ASSIGN_SHIFT_LEFT, TokenType.OP_ASSIGN_SHIFT_RIGHT };
            this.unary_operators = new TokenType[]{ TokenType.OP_SUM, TokenType.OP_SUBSTRACT, TokenType.OP_NEGATION, TokenType.OP_BITS_COMPLEMENT, TokenType.OP_MULTIPLICATION, TokenType.OP_INC_MM, TokenType.OP_INC_PP };
            this.expression_operators = is_as_operators.Concat(multiplicative_operators).Concat(shift_operators).Concat(equality_operators).Concat(relational_operators).Concat(assignment_operators).Concat(unary_operators).ToArray();
        }

        public void parse(){
            this.currentToken = this.lexer.GetNextToken();
            Code();
        }

        public void Code(){
            printDebug("Code");
            CompilationUnit();
            if(!MatchAny( new TokenType[]{TokenType.EOF} ) )
                ThrowSyntaxException("End of File expected");
        }
    }
}