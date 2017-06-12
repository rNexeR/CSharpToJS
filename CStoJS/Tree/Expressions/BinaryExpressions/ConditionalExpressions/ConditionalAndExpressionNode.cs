using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ConditionalAndExpressionNode: ConditionalExpressionNode
    {
        public ConditionalAndExpressionNode() : base(){

        }

        public ConditionalAndExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}