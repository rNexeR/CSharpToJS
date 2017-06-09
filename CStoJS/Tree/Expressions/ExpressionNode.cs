namespace CStoJS.Tree
{
    public abstract class ExpressionNode : VariableInitializer
    {
        public ExpressionNode(){
            
        }

        public abstract TypeDeclarationNode EvaluateType();
    }
}