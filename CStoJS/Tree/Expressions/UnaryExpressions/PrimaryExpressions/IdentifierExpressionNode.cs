using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class IdentifierExpressionNode : PrimaryExpressionNode
    {
        private Token token;
        private IdentifierExpressionNode left;

        public IdentifierExpressionNode() : base(){

        }

        public IdentifierExpressionNode(Token token)
        {
            this.token = token;
            this.left = null;
        }

        public IdentifierExpressionNode(IdentifierExpressionNode left, Token token)
        {
            this.left = left;
            this.token = token;
        }
    }
}