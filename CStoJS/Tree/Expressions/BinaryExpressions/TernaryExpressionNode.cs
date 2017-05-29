namespace CStoJS.Tree
{
    public class TernaryExpressionNode : ExpressionNode
    {
        private ExpressionNode before;
        private ExpressionNode truth;
        private ExpressionNode lie;

        public TernaryExpressionNode() : base(){

        }

        public TernaryExpressionNode(ExpressionNode before, ExpressionNode truth, ExpressionNode lie)
        {
            this.before = before;
            this.truth = truth;
            this.lie = lie;
        }
    }
}