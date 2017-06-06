using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArithmeticDivisionExpressionNode: ArithmeticExpressionNode
    {
        public ArithmeticDivisionExpressionNode()
        {

        }

        public ArithmeticDivisionExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
        
    }
}