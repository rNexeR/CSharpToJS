using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class IfStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode conditional;
        public StatementNode body;
        public ElseNode elseBody;

        public IfStatementNode(){

        }

        public IfStatementNode(ExpressionNode conditional, StatementNode body, ElseNode elseBody){
            this.conditional = conditional;
            this.body = body;
            this.elseBody = elseBody;
        }

        public override TypeDeclarationNode EvaluateSemantic(Semantic.API api, Semantic.ContextManager context_manager){
            var cond_tye = conditional.EvaluateType(api, context_manager);
            if(!(conditional is ConditionalExpressionNode) && cond_tye.ToString() != "BoolType")
                throw new SemanticException("Conditional in If Statement must return a bool.");

            context_manager.Push(new Context(ContextType.IF_CONTEXT));

            var body_ret = body.EvaluateSemantic(api, context_manager);
            context_manager.Pop();
            context_manager.Push(new Context(ContextType.IF_CONTEXT));
            var else_ret = elseBody == null ? null : elseBody.EvaluateSemantic(api, context_manager);

            context_manager.Pop();

            if(body_ret == null && else_ret == null)
                return null;
            else if(body_ret == null){
                return else_ret;
            }else if(else_ret == null){
                return body_ret;
            }else{
                if(body_ret.ToString() != else_ret.ToString())
                    throw new SemanticException($"Multiple return type detected. {body_ret} and {else_ret}.");
                return body_ret;
            }
        }
    }
}