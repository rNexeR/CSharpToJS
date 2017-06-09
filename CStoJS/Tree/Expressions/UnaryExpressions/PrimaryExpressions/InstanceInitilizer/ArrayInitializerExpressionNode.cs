namespace CStoJS.Tree
{
    public class ArrayInitializerExpressionNode : InstanceInitilizerExpressionNode
    {
        public ArrayInitializerNode initializer;
        private readonly ArrayType arr;

        public ArrayInitializerExpressionNode(ArrayType arr, ArrayInitializerNode initializer)
        {
            this.arr = arr;
            this.initializer = initializer;
        }

        public ArrayInitializerExpressionNode()
        {

        }
    }
}