using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ParenthesizedExpressionNode : PrimaryExpressionNode
    {
        public List<ExpressionNode> expressionNode;

        public ParenthesizedExpressionNode(List<ExpressionNode> expressionNode)
        {
            this.expressionNode = expressionNode;
        }

        public ParenthesizedExpressionNode(){
            
        }
    }
}