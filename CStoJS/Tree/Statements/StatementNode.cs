using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public abstract class StatementNode
    {
        public StatementNode()
        {

        }

        public abstract TypeDeclarationNode EvaluateSemantic(API api, ContextManager context_manager);
        // {
        //     return null;
        // }

        public virtual void GenerateCode(Outputs.IOutput output, API api){
            output.WriteStringLine("\t\t(StatementNotImplemented)");
        }
    }
}