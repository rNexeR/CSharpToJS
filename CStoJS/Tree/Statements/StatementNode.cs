using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public abstract class StatementNode
    {
        public StatementNode()
        {

        }

        public void Evaluate(API api, ContextManager context_manager, TypeDeclarationNode returnType) { }
    }
}