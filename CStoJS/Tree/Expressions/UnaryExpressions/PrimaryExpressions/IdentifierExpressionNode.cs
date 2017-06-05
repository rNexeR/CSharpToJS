using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class IdentifierExpressionNode : PrimaryExpressionNode
    {
        public Token token;

        public IdentifierExpressionNode() : base(){

        }

        public IdentifierExpressionNode(Token token)
        {
            this.token = token;
        }
    }
}