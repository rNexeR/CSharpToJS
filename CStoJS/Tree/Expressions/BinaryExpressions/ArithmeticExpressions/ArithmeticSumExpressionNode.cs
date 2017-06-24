using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArithmeticSumExpressionNode: ArithmeticExpressionNode
    {
        public ArithmeticSumExpressionNode()
        {
            this.InitializeRules();
        }

        public ArithmeticSumExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
            this.InitializeRules();
        }

        public void InitializeRules(){
            this.rules["IntType,IntType"] = new IntType();
            this.rules["IntType,CharType"] = new IntType();
            this.rules["CharType,IntType"] = new IntType();
            this.rules["CharType,CharType"] = new IntType();
            
            this.rules["IntType,FloatType"] = new FloatType();
            this.rules["FloatType,FloatType"] = new FloatType();
            this.rules["FloatType,IntType"] = new FloatType();
            this.rules["FloatType,CharType"] = new FloatType();
            this.rules["CharType,FloatType"] = new IntType();

            this.rules["StringType,CharType"] = new StringType();
            this.rules["StringType,StringType"] = new StringType();
            this.rules["StringType,IntType"] = new StringType();
            this.rules["StringType,FloatType"] = new StringType();
            this.rules["CharType,StringType"] = new StringType();
            this.rules["IntType,StringType"] = new StringType();
        }
    }
}