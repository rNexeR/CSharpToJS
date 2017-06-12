using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ConditionalEqualExpressionNode: ConditionalExpressionNode
    {
        public ConditionalEqualExpressionNode() : base(){

        }

        public ConditionalEqualExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}