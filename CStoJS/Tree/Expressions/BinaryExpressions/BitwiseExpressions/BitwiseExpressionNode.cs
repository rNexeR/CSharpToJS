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

        public void InitializeRules()
        {
            this.rules["IntType,IntType"] = new IntType();
            this.rules["IntType,CharType"] = new IntType();
            this.rules["CharType,CharType"] = new IntType();
        }
    }
}