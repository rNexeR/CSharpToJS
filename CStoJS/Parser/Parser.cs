using CStoJS.Utils;

namespace CStoJS.Parser
{
    public partial class Parser
    {
        private Lexer lexer;
        private Token currentToken;
        private TokenType[] encapsulation_modifiers;
        private TokenType[] class_modifiers;

        public Parser(Lexer lexer){
            this.lexer = lexer;
            this.InitializeModifiers();
        }

        public void InitializeModifiers(){
            this.encapsulation_modifiers = { TokenType.PRIVATE_KEYWORD, TokenType.PROTECTED_KEYWORD, TokenType.PUBLIC_KEYWORD };
            this.class_modifiers = { TokenType.ABSTRACT_KEYWORD };
        }

        public void parse(){
            this.currentToken = this.lexer.GetNextToken();
            Code();
        }

        public static bool MatchAny( TokenType[] expectedList){
			return expectedList.Contains(currentToken.type);
		}

        public bool MatchExactly(TokenType[] expectedTokens){
            foreach(var expected in expectedTokens){
                if(currentToken.type != expected){
                    throw new SyntaxException($"{expected} expected");
                    currentToken = this.lexer.GetNextToken();
                }
            }
            return true;
        }

        public void OptionalMatchExactly(TokenType[] expectedTokens){
            foreach(var expected in expectedTokens){
                if(currentToken.type != expected){
                    if(expected == expectedTokens[0])
                        return;
                    throw new SyntaxException($"{expected} expected");
                    currentToken = this.lexer.GetNextToken();
                }
            }
            return true;
        }

        public void Code(){
            CompilationUnit();
            if(!MatchAny(currentToken, {TokenType.EOF} ) )
                throw new SyntaxException("End of File expected");
        }
    }
}