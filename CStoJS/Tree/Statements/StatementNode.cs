using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public abstract class StatementNode
    {
        public StatementNode()
        {

        }

        public virtual TypeDeclarationNode EvaluateSemantic(API api, ContextManager context_manager)
        {
            return null;
        }
    }
}