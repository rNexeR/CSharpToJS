namespace CStoJS.Tree
{
    public class ArrayInitializerExpressionNode : InstanceInitilizerExpressionNode
    {
        private ArrayType arr;
        private ArrayInitializerNode initializer;

        public ArrayInitializerExpressionNode(ArrayType arr, ArrayInitializerNode initializer)
        {
            this.arr = arr;
            this.initializer = initializer;
        }

        public ArrayInitializerExpressionNode(){
            
        }
    }
}