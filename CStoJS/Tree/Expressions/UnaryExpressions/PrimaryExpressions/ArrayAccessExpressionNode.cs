using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ArrayAccessExpressionNode : PrimaryExpressionNode
    {
        public List<ArrayAccessNode> indexes;
        public ExpressionNode left;

        public ArrayAccessExpressionNode(List<ArrayAccessNode> indexes)
        {
            this.indexes = indexes;
        }

        public ArrayAccessExpressionNode(){
            
        }

        public ArrayAccessExpressionNode(ExpressionNode left, List<ArrayAccessNode> indexes)
        {
            this.left = left;
            this.indexes = indexes;
        }
    }
}