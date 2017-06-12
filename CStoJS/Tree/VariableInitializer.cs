using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public abstract class VariableInitializer
    {
        public VariableInitializer(){
            
        }

        public abstract TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man);
    }
}