using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ArrayAccessNode
    {
        public List<ExpressionNode> exprs;

        public ArrayAccessNode(List<ExpressionNode> exprs)
        {
            this.exprs = exprs;
        }

        public ArrayAccessNode(){
            
        }
    }
}