using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BitwiseAndExpressionNode: BitwiseExpressionNode
    {
        public BitwiseAndExpressionNode(){

        }

        public BitwiseAndExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }
    }
}