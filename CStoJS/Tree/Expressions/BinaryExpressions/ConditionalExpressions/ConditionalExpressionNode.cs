using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public abstract class ConditionalExpressionNode : BinaryExpressionNode
    {
        public ConditionalExpressionNode() : base(){
            
        }
        public ConditionalExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}