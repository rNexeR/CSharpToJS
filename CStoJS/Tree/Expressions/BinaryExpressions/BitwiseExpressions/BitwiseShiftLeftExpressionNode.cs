using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BitwiseShiftLeftExpressionNode: BitwiseExpressionNode
    {
        public BitwiseShiftLeftExpressionNode()
        {

        }

        public BitwiseShiftLeftExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}