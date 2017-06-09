using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArithmeticSumExpressionNode: ArithmeticExpressionNode
    {
        public ArithmeticSumExpressionNode()
        {

        }

        public ArithmeticSumExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}