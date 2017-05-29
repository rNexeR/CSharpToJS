using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ArrayAccessNode
    {
        private List<ExpressionNode> exprs;

        public ArrayAccessNode(List<ExpressionNode> exprs)
        {
            this.exprs = exprs;
        }

        public ArrayAccessNode(){
            
        }
    }
}