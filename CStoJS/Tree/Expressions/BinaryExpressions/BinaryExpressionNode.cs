using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BinaryExpressionNode : ExpressionNode
    {
        public ExpressionNode left;
        public Token operador;
        public ExpressionNode right;

        public BinaryExpressionNode(){

        }

        public BinaryExpressionNode(ExpressionNode left, Token operador, ExpressionNode right){
            this.left = left;
            this.operador = operador;
            this.right = right;
        }
    }
}