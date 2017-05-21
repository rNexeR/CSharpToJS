using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;
using System.Linq;
using System;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        public void ThrowSyntaxException(string msg){
            throw new SyntaxException(msg + $" {currentToken.type} provided" + ". [" + currentToken.row + "," + currentToken.column + "]");
        }

        public bool Match( TokenType expected){
			return expected == currentToken.type;
		}

        public void MatchOne(TokenType[] expectedTokens, string msg){
            foreach(var expected in expectedTokens){
                if(currentToken.type == expected){
                    currentToken = this.lexer.GetNextToken();
                    return;
                }
                
            }
            ThrowSyntaxException(msg);
        }

        public bool MatchAny( TokenType[] expectedList){
			return expectedList.Contains(currentToken.type);
		}

        public bool MatchExactly(TokenType[] expectedTokens){
            foreach(var expected in expectedTokens){
                if(currentToken.type != expected){
                    ThrowSyntaxException($"{expected} expected");
                }
                currentToken = this.lexer.GetNextToken();
            }
            return true;
        }

        public bool OptionalMatchExactly(TokenType[] expectedTokens){
            foreach(var expected in expectedTokens){
                if(currentToken.type != expected){
                    if(expected == expectedTokens[0])
                        return false;
                    ThrowSyntaxException($"{expected} expected");
                }
                currentToken = this.lexer.GetNextToken();
            }
            return true;
        }

        public void printDebug(string msg){
            if (enableDebug) Console.WriteLine(msg + " at " + currentToken.row + "," + currentToken.column + $". [{currentToken.type} = {currentToken.lexema}]");
        }
    }
}