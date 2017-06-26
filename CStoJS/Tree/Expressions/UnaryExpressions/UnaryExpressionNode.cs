using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public abstract class UnaryExpressionNode : ExpressionNode
    {
        public UnaryExpressionNode(){
            
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man){
            return this.EvaluateType(api, ctx_man, null);
        }

        public abstract TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man);

        // public override void GenerateCode(Outputs.IOutput output, API api){}
    }
}