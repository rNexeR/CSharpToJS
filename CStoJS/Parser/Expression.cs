using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries{
	public partial class Parser
    {
        void Expression(){
            Literals();
        }

        void Literals(){
            if( MatchAny(new TokenType[] { TokenType.LITERAL_CHAR, TokenType.LITERAL_FLOAT, TokenType.LITERAL_INT, TokenType.LITERAL_STRING, TokenType.LITERAL_STRING_VERBATIM, TokenType.TRUE_KEYWORD, TokenType.FALSE_KEYWORD }) ){
                currentToken = lexer.GetNextToken();
            }else{
                ThrowSyntaxException("Literal Expected");
            }
        }
    }
}