using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BitwiseExpressionNode : BinaryExpressionNode
    {
        public BitwiseExpressionNode() : base(){
            this.InitializeRules();
        }
        public BitwiseExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
            this.InitializeRules();
        }

        public void InitializeRules()
        {
            this.rules["IntType,IntType"] = new IntType();
            this.rules["IntType,CharType"] = new IntType();
            this.rules["CharType,CharType"] = new IntType();
            this.rules["CharType,IntType"] = new IntType();
        }
    }
}