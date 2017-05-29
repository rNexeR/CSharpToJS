namespace CStoJS.Tree
{
    public class ParenthesizedExpressionNode : PrimaryExpressionNode
    {
        private ExpressionNode expressionNode;

        public ParenthesizedExpressionNode(ExpressionNode expressionNode)
        {
            this.expressionNode = expressionNode;
        }

        public ParenthesizedExpressionNode(){
            
        }
    }
}