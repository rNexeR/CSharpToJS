using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ConditionalOrExpressionNode : ConditionalExpressionNode
    {
        public ConditionalOrExpressionNode() : base(){

        }

        public ConditionalOrExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}