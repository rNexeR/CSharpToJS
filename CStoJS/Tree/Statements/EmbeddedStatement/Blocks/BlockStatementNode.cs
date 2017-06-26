using System.Collections.Generic;
using CStoJS.Exceptions;

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

        public override TypeDeclarationNode EvaluateSemantic(Semantic.API api, Semantic.ContextManager context_manager){
            TypeDeclarationNode ret = null;
            foreach(var stmt in this.statements){
                var st_ret = stmt.EvaluateSemantic(api, context_manager);
                if(ret == null && st_ret != null)
                    ret = st_ret;
                if(ret != null && st_ret != null && ret.ToString() != st_ret.ToString())
                    throw new SemanticException($"Multiple return type detected. {ret} and {st_ret}.");
            }
            return ret;
        }

        public override void GenerateCode(Outputs.IOutput output, Semantic.API api){
            foreach(var stmt in this.statements){
                stmt.GenerateCode(output, api);
            }
        }
    }
}