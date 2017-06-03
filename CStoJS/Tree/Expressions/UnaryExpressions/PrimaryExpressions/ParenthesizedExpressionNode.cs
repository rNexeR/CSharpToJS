namespace CStoJS.Tree
{
    public class ParenthesizedExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode expressionNode;

        public ParenthesizedExpressionNode(ExpressionNode expressionNode)
        {
            this.expressionNode = expressionNode;
        }

        public ParenthesizedExpressionNode(){
            
        }
    }
}