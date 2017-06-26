namespace CStoJS.Tree
{
    public class StatementExpressionNode : StatementNode
    {
        public ExpressionNode expressionNode;

        public StatementExpressionNode(){
            
        }

        public StatementExpressionNode(ExpressionNode expressionNode)
        {
            this.expressionNode = expressionNode;
        }

        public override TypeDeclarationNode EvaluateSemantic(Semantic.API api, Semantic.ContextManager context_manager){
            this.expressionNode.EvaluateType(api, context_manager);
            return null;
        }

        public override void GenerateCode(Outputs.IOutput output, Semantic.API api){
            output.WriteString("\t\t");
            this.expressionNode.GenerateCode(output, api);
            output.WriteStringLine(";");
        }
    }
}