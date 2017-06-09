using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BitwiseExpressionNode : BinaryExpressionNode
    {
        public BitwiseExpressionNode() : base(){
            
        }
        public BitwiseExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}