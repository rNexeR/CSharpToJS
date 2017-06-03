using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class AccessMemoryExpressionNode : PrimaryExpressionNode
    {
        public Token token;
        public ExpressionNode left;

        public AccessMemoryExpressionNode(Token token)
        {
            this.token = token;
        }

        public AccessMemoryExpressionNode(ExpressionNode identifierExpressionNode, Token token)
        {
            this.left = identifierExpressionNode;
            this.token = token;
        }

        public AccessMemoryExpressionNode(){
            
        }
    }
}