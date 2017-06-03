using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        public List<ArgumentNode> args;
        public ExpressionNode left;

        public FunctionCallExpressionNode(ExpressionNode left, List<ArgumentNode> args)
        {
            this.left = left;
            this.args = args;
        }

        public FunctionCallExpressionNode(){
            
        }
    }
}