using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class AccessMemoryExpressionNode : PrimaryExpressionNode
    {
        private AccessMemoryExpressionNode identifierExpressionNode;
        private Token token;

        public AccessMemoryExpressionNode(Token token)
        {
            this.token = token;
        }

        public AccessMemoryExpressionNode(AccessMemoryExpressionNode identifierExpressionNode, Token token)
        {
            this.identifierExpressionNode = identifierExpressionNode;
            this.token = token;
        }
    }
}