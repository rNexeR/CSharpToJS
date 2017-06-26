using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public abstract class VariableInitializer
    {

        public TypeDeclarationNode returnType;
        public VariableInitializer()
        {

        }

        public abstract TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man);
        public abstract void GenerateCode(Outputs.IOutput output, API api);
        // {
        //     output.WriteString("(notImplemented)");
        // }
    }
}