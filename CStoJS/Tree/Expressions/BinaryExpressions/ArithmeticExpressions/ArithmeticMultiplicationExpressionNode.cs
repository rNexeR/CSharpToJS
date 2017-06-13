using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArithmeticMultiplicationExpressionNode: ArithmeticExpressionNode
    {
        public ArithmeticMultiplicationExpressionNode()
        {
            this.InitializeRules();
        }

        public ArithmeticMultiplicationExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
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
        }
    }
}