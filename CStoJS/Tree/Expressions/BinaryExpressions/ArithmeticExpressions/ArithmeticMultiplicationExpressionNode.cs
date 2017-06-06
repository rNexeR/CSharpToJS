using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArithmeticMultiplicationExpressionNode: ArithmeticExpressionNode
    {
        public ArithmeticMultiplicationExpressionNode()
        {

        }

        public ArithmeticMultiplicationExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}