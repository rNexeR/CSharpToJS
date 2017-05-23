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

        public bool ConsumeOnMatch( TokenType expected){
			if( expected == currentToken.type){
                ConsumeToken();
                // Console.WriteLine($"\tCOM {expected} == CT {currentToken.type}");
                return true;
            }
            return false;
		}

        public bool MatchExactly( TokenType expected){
			if( expected == currentToken.type){
                ConsumeToken();
                return true;
            }
            ThrowSyntaxException($"{expected} expected");
            return false;
		}

        public void MatchOne(TokenType[] expectedTokens, string msg){
            foreach(var expected in expectedTokens){
                if(currentToken.type == expected){
                    ConsumeToken();
                    return;
                }
                
            }
            ThrowSyntaxException(msg);
        }

        public bool MatchAny( TokenType[] expectedList){
			return expectedList.Contains(currentToken.type);
		}

        public bool MatchAndComsumeAny( TokenType[] expectedList){
			if( expectedList.Contains(currentToken.type) ){
                ConsumeToken();
                return true;
            }
            return false;
		}

        public bool MatchExactly(TokenType[] expectedTokens){
            foreach(var expected in expectedTokens){
                if(currentToken.type != expected){
                    ThrowSyntaxException($"{expected} expected");
                }
                ConsumeToken();
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
                ConsumeToken();
            }
            return true;
        }

        public bool ConsumeOnMatchLA(TokenType expected){
            var tok = currentToken;
            if(Match(expected)){
                ConsumeToken();
                this.lookAhead = this.lookAhead.Concat(new Token[]{tok}).ToArray();
                return true;
            }
            return false;
        }
        
        public bool MatchAndComsumeAnyLA(TokenType[] expectedList){
            var tok = currentToken;
            if(MatchAny(expectedList)){
                ConsumeToken();
                this.lookAhead = this.lookAhead.Concat(new Token[]{tok}).ToArray();
                return true;
            }
            return false;
        }

        public void printDebug(string msg){
            if (enableDebug) {
                Console.WriteLine(msg + " at " + currentToken.row + "," + currentToken.column + $". [{currentToken.type} = {currentToken.lexema}]");
                // if(!(lookAhead.Length > 0))
                //     Console.WriteLine(msg + " at " + currentToken.row + "," + currentToken.column + $". [{currentToken.type} = {currentToken.lexema}] [LA <{lookAhead.Length}> ]");
                // else
                //     Console.WriteLine(msg + " at " + currentToken.row + "," + currentToken.column + $". [{currentToken.type} = {currentToken.lexema}] [LA <{lookAhead.Length}> <{lookAhead[0]}> ]");
            }
        }

        private void ConsumeToken(){
            if(lookAheadBack && lookAhead.Length > 0){
                currentToken = lookAhead[0];
                var temp = lookAhead.ToList();
                temp.RemoveAt(0);
                lookAhead = temp.ToArray();
                if(lookAhead.Length == 0)
                    lookAheadBack = false;
            }else{
                currentToken = lexer.GetNextToken();
            }
        }

        private void RollbackLA(){
            if(lookAhead.Length == 0)
                return;
            lookAheadBack = true;
            var temp = lookAhead.ToList();
            temp.Add(currentToken);
            lookAhead = temp.ToArray();
            ConsumeToken(); 
        }
    }
}