using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        private ExpressionNode left;
        private List<ArgumentNode> args;

        public FunctionCallExpressionNode(ExpressionNode left, List<ArgumentNode> args)
        {
            this.left = left;
            this.args = args;
        }

        public FunctionCallExpressionNode(){
            
        }
    }
}