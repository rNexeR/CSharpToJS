using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class UnaryStatement : UnaryExpressionNode
    {
        public List<ExpressionNode> expression;

        public UnaryStatement(){

        }

        public UnaryStatement(List<ExpressionNode> expression)
        {
            this.expression = expression;
        }
    }
}