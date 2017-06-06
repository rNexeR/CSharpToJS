using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ConditionalRelationalExpressionNode: ConditionalExpressionNode
    {
        public ConditionalRelationalExpressionNode()
        {

        }

        public ConditionalRelationalExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}