using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        public List<ArgumentNode> args;

        public FunctionCallExpressionNode(List<ArgumentNode> args)
        {
            this.args = args;
        }

        public FunctionCallExpressionNode(){
            
        }
    }
}