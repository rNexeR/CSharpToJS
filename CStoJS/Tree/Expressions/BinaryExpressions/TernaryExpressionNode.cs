namespace CStoJS.Tree
{
    public class TernaryExpressionNode : ExpressionNode
    {
        private ExpressionNode conditionalExpression;
        private ExpressionNode trueExpression;
        private ExpressionNode falseExpression;

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