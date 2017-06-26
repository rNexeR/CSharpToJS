using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public abstract class ConditionalExpressionNode : BinaryExpressionNode
    {
        public ConditionalExpressionNode() : base(){
            this.InitializeRules();
            
        }
        public ConditionalExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
            this.InitializeRules();
        }

        public void InitializeRules()
        {
            this.rules["BoolType,BoolType"] = new BoolType();
            this.rules["IntType,CharType"] = new BoolType();
            this.rules["CharType,IntType"] = new BoolType();
            this.SameTypeValid = true;
        }

        public override TypeDeclarationNode EvaluateType(Semantic.API api, Semantic.ContextManager ctx_man){
            base.EvaluateType(api, ctx_man);
            this.returnType = new BoolType();
            return new BoolType();
        }
    }
}