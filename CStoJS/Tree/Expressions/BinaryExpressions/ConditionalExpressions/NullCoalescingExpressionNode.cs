using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class NullCoalescingExpressionNode : ConditionalExpressionNode
    {
        public NullCoalescingExpressionNode(){

        }

        public NullCoalescingExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}