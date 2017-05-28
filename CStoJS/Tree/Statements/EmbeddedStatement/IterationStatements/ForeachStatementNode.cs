namespace CStoJS.Tree
{
    public class ForeachStatementNode : EmbeddedStatementNode
    {
        public LocalVariableNode localVariable;
        public ExpressionNode collection;

        public ForeachStatementNode(){

        }

        public ForeachStatementNode(LocalVariableNode localVariable, ExpressionNode collection){
            this.localVariable = localVariable;
            this.collection = collection;
        }
    }
}