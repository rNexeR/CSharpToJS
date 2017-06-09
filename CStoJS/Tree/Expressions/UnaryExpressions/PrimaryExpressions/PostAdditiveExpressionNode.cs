using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        public Token operador;
        public ExpressionNode indentifier;

        public PostAdditiveExpressionNode(Token token)
        {
            this.operador = token;
        }

        public PostAdditiveExpressionNode(){
            
        }
    }
}