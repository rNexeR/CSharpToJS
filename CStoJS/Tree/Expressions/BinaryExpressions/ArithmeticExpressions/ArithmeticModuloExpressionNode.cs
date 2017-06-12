using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArithmeticModuloExpressionNode: ArithmeticExpressionNode
    {
        public ArithmeticModuloExpressionNode()
        {
            this.InitializeRules();
        }

        public ArithmeticModuloExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
            this.InitializeRules();
        }

        public void InitializeRules(){
            this.rules["IntType,IntType"] = new IntType();
            this.rules["FloatType,FloatType"] = new FloatType();
            this.rules["FloatType,IntType"] = new FloatType();
            this.rules["IntType,FloatType"] = new FloatType();
            this.rules["IntType,CharType"] = new IntType();
            this.rules["CharType,IntType"] = new IntType();
            this.rules["CharType,CharType"] = new IntType();
        }
    }
}