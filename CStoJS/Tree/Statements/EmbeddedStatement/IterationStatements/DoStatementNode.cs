namespace CStoJS.Tree
{
    public class DoStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode conditional;
        public EmbeddedStatementNode body;

        public DoStatementNode(){

        }

        public DoStatementNode(ExpressionNode conditional, EmbeddedStatementNode body){
            this.conditional = conditional;
            this.body = body;
        }
    }
}