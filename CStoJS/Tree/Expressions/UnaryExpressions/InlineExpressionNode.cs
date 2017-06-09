using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class InlineExpressionNode : UnaryExpressionNode
    {
        public List<ExpressionNode> expressions;

        public InlineExpressionNode()
        {

        }

        public InlineExpressionNode(List<ExpressionNode> expression)
        {
            this.expressions = expression;
        }
    }
}