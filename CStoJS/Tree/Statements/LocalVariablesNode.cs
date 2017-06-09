using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class LocalVariablesNode : StatementNode
    {
        public List<LocalVariableNode> variablesNodes;

        public LocalVariablesNode(){
            this.variablesNodes = new List<LocalVariableNode>();
        }

        public LocalVariablesNode(List<LocalVariableNode> variablesNodes)
        {
            this.variablesNodes = variablesNodes;
        }
    }
}