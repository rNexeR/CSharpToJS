using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class SwitchStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode expressionToEval;
        public List<CaseExpressionNode> cases;

        public SwitchStatementNode(){
            this.cases = new List<CaseExpressionNode>();
        }

        public SwitchStatementNode(ExpressionNode exprToEval, List<CaseExpressionNode> cases){
            this.expressionToEval = exprToEval;
            this.cases = cases;
        }

        public override TypeDeclarationNode EvaluateSemantic(Semantic.API api, Semantic.ContextManager context_manager){
            var eval_type = this.expressionToEval.EvaluateType(api, context_manager);
            context_manager.Push(new Context(ContextType.SWITCH_CONTEXT));
            TypeDeclarationNode ret = null;

            foreach(var _case in this.cases){
                var case_ret = _case.EvaluateSemantic(api, context_manager, eval_type);
                if(case_ret != null && ret == null)
                    ret = case_ret;
                if(case_ret != null && ret != null){
                    if(case_ret.ToString() != ret.ToString()){
                        throw new SemanticException($"Multiple return type detected. {ret} and {case_ret}.");
                    }
                }
                    
            }

            context_manager.Pop();

            return ret;
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            output.WriteString("\t\tswitch(");
            this.expressionToEval.GenerateCode(output, api);
            output.WriteStringLine("){");
            foreach(var _case in this.cases){
                _case.GenerateCode(output, api);
            }
            output.WriteStringLine("\t\t}");
        }
    }
}