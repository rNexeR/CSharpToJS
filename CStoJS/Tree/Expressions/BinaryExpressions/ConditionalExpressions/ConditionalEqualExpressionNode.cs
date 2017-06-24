using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ConditionalEqualExpressionNode: ConditionalExpressionNode
    {
        public ConditionalEqualExpressionNode() : base(){
            // this.SameTypeValid = true;
        }

        public ConditionalEqualExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
            // this.SameTypeValid = true;
        }
    }
}