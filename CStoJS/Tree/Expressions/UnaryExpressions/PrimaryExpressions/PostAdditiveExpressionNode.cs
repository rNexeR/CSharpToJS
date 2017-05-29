using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        private ExpressionNode left;
        private Token token;

        public PostAdditiveExpressionNode(ExpressionNode left, Token token)
        {
            this.left = left;
            this.token = token;
        }
    }
}