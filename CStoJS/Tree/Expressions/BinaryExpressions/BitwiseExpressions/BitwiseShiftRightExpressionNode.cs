using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BitwiseShiftRightExpressionNode: BitwiseExpressionNode
    {
        public BitwiseShiftRightExpressionNode()
        {

        }

        public BitwiseShiftRightExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}