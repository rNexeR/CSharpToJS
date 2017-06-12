using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ConditionalNotEqualExpressionNode: ConditionalExpressionNode
    {
        public ConditionalNotEqualExpressionNode() : base(){

        }

        public ConditionalNotEqualExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}