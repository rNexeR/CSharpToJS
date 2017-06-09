namespace CStoJS.Tree
{
    public class IfStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode conditional;
        public EmbeddedStatementNode body;
        public ElseNode elseBody;

        public IfStatementNode(){

        }

        public IfStatementNode(ExpressionNode conditional, EmbeddedStatementNode body, ElseNode elseBody){
            this.conditional = conditional;
            this.body = body;
            this.elseBody = elseBody;
        }
    }
}