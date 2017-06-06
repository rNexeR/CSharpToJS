using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BitwiseXorExpressionNode: BitwiseExpressionNode
    {
        public BitwiseXorExpressionNode(){

        }

        public BitwiseXorExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}