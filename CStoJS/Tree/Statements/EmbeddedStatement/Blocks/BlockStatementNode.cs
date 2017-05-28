using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class BlockStatementNode : EmbeddedStatementNode
    {
        public List<StatementNode> statements;

        public BlockStatementNode(){

        }

        public BlockStatementNode(List<StatementNode> lista){
            this.statements = lista;
        }
    }
}