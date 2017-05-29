namespace CStoJS.Tree
{
    public class StatementExpressionNode : StatementNode
    {
        private ExpressionNode expressionNode;

        public StatementExpressionNode(ExpressionNode expressionNode)
        {
            this.expressionNode = expressionNode;
        }
    }
}