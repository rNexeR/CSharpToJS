using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class WhileStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode conditional;
        public StatementNode body;

        public WhileStatementNode(){

        }

        public WhileStatementNode(ExpressionNode conditional, StatementNode body){
            this.conditional = conditional;
            this.body = body;
        }

        public override TypeDeclarationNode EvaluateSemantic(Semantic.API api, Semantic.ContextManager context_manager){
            context_manager.Push(new Context(ContextType.WHILE_CONTEXT));
            var cond_tye = conditional.EvaluateType(api, context_manager);
            if(!(conditional is ConditionalExpressionNode) && cond_tye.ToString() != "BoolType")
                throw new SemanticException("Conditional in If Statement must return a bool.");

            var ret = body.EvaluateSemantic(api, context_manager);
            context_manager.Pop();

            return ret;
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            output.WriteString("\t\t\twhile(");
            this.conditional.GenerateCode(output, api);
            output.WriteString(")");
            if(this.body != null){
                output.WriteStringLine("{");
                this.body.GenerateCode(output, api);
                output.WriteStringLine("\t\t\t}");
            }else{
                output.WriteStringLine(";");
            }
        }
    }
}