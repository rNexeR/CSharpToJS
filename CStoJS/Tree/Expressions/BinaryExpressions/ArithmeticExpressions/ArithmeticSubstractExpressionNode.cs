using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArithmeticSubstractExpressionNode: ArithmeticExpressionNode
    {
        public ArithmeticSubstractExpressionNode()
        {

        }

        public ArithmeticSubstractExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}