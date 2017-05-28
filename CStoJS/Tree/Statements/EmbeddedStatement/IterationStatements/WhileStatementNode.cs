namespace CStoJS.Tree
{
    public class WhileStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode conditional;
        public EmbeddedStatementNode body;

        public WhileStatementNode(){

        }

        public WhileStatementNode(ExpressionNode conditional, EmbeddedStatementNode body){
            this.conditional = conditional;
            this.body = body;
        }
    }
}