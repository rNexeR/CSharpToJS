namespace CStoJS.Tree
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeDeclarationNode targetType;
        public ExpressionNode expression;

        public CastingExpressionNode(){

        }

        public CastingExpressionNode(TypeDeclarationNode targetType, ExpressionNode expression){
            this.targetType = targetType;
            this.expression = expression;
        }
    }
}