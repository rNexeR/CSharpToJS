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

        public override TypeDeclarationNode EvaluateSemantic(Semantic.API api, Semantic.ContextManager context_manager){
            foreach(var variable in this.variablesNodes){
                variable.EvaluateSemantic(api, context_manager);
            }
            return null;
        }
    }
}