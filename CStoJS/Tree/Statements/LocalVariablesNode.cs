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

        public override void GenerateCode(Outputs.IOutput output, Semantic.API api){
            var i = 0;
            foreach(var variable in this.variablesNodes){
                variable.GenerateCode(output, api);
                // if(i != this.variablesNodes.Count -1 && this.variablesNodes.Count > 1)
                //     output.WriteString(", ");
            }
        }
    }
}