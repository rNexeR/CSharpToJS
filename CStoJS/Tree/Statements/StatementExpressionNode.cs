namespace CStoJS.Tree
{
    public class StatementExpressionNode : StatementNode
    {
        public ExpressionNode expressionNode;

        public StatementExpressionNode(){
            
        }

        public StatementExpressionNode(ExpressionNode expressionNode)
        {
            this.expressionNode = expressionNode;
        }
    }
}