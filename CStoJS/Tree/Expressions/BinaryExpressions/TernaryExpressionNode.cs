namespace CStoJS.Tree
{
    public class TernaryExpressionNode : ExpressionNode
    {
        public ExpressionNode conditionalExpression;
        public ExpressionNode trueExpression;
        public ExpressionNode falseExpression;

        public TernaryExpressionNode() : base(){

        }

        public TernaryExpressionNode(ExpressionNode before, ExpressionNode truth, ExpressionNode lie)
        {
            this.conditionalExpression = before;
            this.trueExpression = truth;
            this.falseExpression = lie;
        }
    }
}