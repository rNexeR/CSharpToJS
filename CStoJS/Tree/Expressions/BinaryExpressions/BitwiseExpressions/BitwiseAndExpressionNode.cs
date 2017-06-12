using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BitwiseAndExpressionNode: BitwiseExpressionNode
    {
        public BitwiseAndExpressionNode():base(){

        }

        public BitwiseAndExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}