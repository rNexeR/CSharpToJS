namespace CStoJS.Tree
{
    public class ElseNode
    {
        public EmbeddedStatementNode body;

        public ElseNode(){

        }

        public ElseNode(EmbeddedStatementNode body){
            this.body = body;
        }
    }
}