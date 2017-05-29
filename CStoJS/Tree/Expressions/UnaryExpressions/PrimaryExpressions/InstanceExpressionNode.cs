namespace CStoJS.Tree
{
    public class InstanceExpressionNode : PrimaryExpressionNode
    {
        private TypeDeclarationNode type;
        private InstanceInitilizerExpressionNode initializer;

        public InstanceExpressionNode(TypeDeclarationNode type, InstanceInitilizerExpressionNode initializer)
        {
            this.type = type;
            this.initializer = initializer;
        }
    }
}