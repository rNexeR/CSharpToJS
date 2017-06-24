using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ForStatementNode : EmbeddedStatementNode
    {
        public List<StatementNode> initializerExpression;
        public ExpressionNode conditionalExpression;
        public List<StatementNode> incrementExpression;
        public StatementNode body;

        public ForStatementNode(){
            this.initializerExpression = new List<StatementNode>();
        }

        public ForStatementNode(List<StatementNode> initializer, ExpressionNode conditional, List<StatementNode> increment, StatementNode body){
            this.initializerExpression = initializer;
            this.conditionalExpression = conditional;
            this.incrementExpression = increment;
            this.body = body;
        }

        public override TypeDeclarationNode EvaluateSemantic(Semantic.API api, Semantic.ContextManager context_manager){
            context_manager.Push(new Context(ContextType.FOR_CONTEXT));
            foreach(var st in initializerExpression){
                st.EvaluateSemantic(api, context_manager);
            }

            var cond_tye = conditionalExpression.EvaluateType(api, context_manager);
            if(!(conditionalExpression is ConditionalExpressionNode) && cond_tye.ToString() != "BoolType")
                throw new SemanticException("Conditional in If Statement must return a bool.");
            
            foreach(var st in incrementExpression){
                st.EvaluateSemantic(api, context_manager);
            }
            var ret = body.EvaluateSemantic(api, context_manager);
            context_manager.Pop();
            return ret;

        }
    }
}