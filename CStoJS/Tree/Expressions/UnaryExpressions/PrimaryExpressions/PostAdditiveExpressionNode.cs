using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        public Token token;

        public PostAdditiveExpressionNode(Token token)
        {
            this.token = token;
        }

        public PostAdditiveExpressionNode(){
            
        }
    }
}