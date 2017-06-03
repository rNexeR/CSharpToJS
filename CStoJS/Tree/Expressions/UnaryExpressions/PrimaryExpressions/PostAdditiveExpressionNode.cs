using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode left;
        public Token token;

        public PostAdditiveExpressionNode(ExpressionNode left, Token token)
        {
            this.left = left;
            this.token = token;
        }

        public PostAdditiveExpressionNode(){
            
        }
    }
}