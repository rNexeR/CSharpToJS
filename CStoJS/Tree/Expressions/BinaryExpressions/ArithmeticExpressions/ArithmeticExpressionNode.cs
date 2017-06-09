using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArithmeticExpressionNode : BinaryExpressionNode
    {
        public ArithmeticExpressionNode() : base(){

        }

        public ArithmeticExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}