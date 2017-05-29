using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ArrayAccessExpressionNode : PrimaryExpressionNode
    {
        private List<ArrayAccessNode> list;

        public ArrayAccessExpressionNode(List<ArrayAccessNode> list)
        {
            this.list = list;
        }
    }
}