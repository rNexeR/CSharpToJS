using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS
{
    public class LocalVariablesNode : StatementNode
    {
        private List<LocalVariableNode> variablesNodes;

        public LocalVariablesNode(){
            this.variablesNodes = new List<LocalVariableNode>();
        }

        public LocalVariablesNode(List<LocalVariableNode> variablesNodes)
        {
            this.variablesNodes = variablesNodes;
        }
    }
}