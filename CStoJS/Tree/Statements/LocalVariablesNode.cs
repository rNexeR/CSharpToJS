using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS
{
    public class LocalVariablesNode : StatementNode
    {
        private List<LocalVariableNode> variablesNodes;

        public LocalVariablesNode(List<LocalVariableNode> variablesNodes)
        {
            this.variablesNodes = variablesNodes;
        }
    }
}