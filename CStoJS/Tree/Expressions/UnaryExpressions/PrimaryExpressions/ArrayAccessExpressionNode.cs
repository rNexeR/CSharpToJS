using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ArrayAccessExpressionNode : PrimaryExpressionNode
    {
        public List<ArrayAccessNode> indexes;

        public ArrayAccessExpressionNode(List<ArrayAccessNode> indexes)
        {
            this.indexes = indexes;
        }

        public ArrayAccessExpressionNode(){
            
        }
    }
}