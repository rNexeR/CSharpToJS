using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BitwiseOrExpressionNode: BitwiseExpressionNode
    {
        public BitwiseOrExpressionNode(): base(){

        }

        public BitwiseOrExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
        
    }
}