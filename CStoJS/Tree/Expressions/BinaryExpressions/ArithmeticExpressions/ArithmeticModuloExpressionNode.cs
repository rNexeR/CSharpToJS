using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArithmeticModuloExpressionNode: ArithmeticExpressionNode
    {
        public ArithmeticModuloExpressionNode()
        {

        }

        public ArithmeticModuloExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}