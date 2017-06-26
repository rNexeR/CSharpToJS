using System;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class IdentifierExpressionNode : PrimaryExpressionNode
    {
        public Token token;
        private bool add_this = false;
        private bool is_class = false;
        private string class_name = "";

        public IdentifierExpressionNode() : base()
        {

        }

        public IdentifierExpressionNode(Token token)
        {
            this.token = token;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            // var _usings = ctx_man.GetCurrentNamespaceUsings();
            // var class_name = Utils.GetClassName(token.lexema, _usings, api);
            var ctx_man = st_ctx_man is null ? class_ctx_man : st_ctx_man;
            var usings = class_ctx_man.GetCurrentNamespaceUsings();
            is_class = false;
            class_name = Utils.GetClassName(this.token.lexema, usings, api);
            if (class_name != "")
                is_class = true;
            if(token.lexema == "FRESHMAN")
                Console.WriteLine("");
            if (!ctx_man.VariableExists(token.lexema) && !is_class /*&& api.GetTypeDeclaration(class_name) == null*/)
                if (!ctx_man.IsStaticContext())
                    throw new SemanticException($"Variable {token.lexema} doesn't exists in type({ctx_man.GetCurrentClass()}), is inaccesible due to its protection lever or is declared as static", token);
                else
                    throw new SemanticException($"Variable {token.lexema} doesn't exists in type({ctx_man.GetCurrentClass()}) or is not declared as static", token);

            if (is_class)
                return api.GetTypeDeclaration(class_name);

            this.returnType = ctx_man.GetVariableType(token.lexema);
            if(returnType.is_static && st_ctx_man == null)
                this.class_name = class_ctx_man.GetCurrentClass();
            else
                returnType.is_static = false;
            this.add_this = returnType.is_in_class && st_ctx_man == null && !this.token.lexema.StartsWith("this.") &&  !this.token.lexema.StartsWith("base.");

            return returnType;
        }

        public override void GenerateCode(Outputs.IOutput output, API api)
        {
            if(token.lexema.Contains("base."))
                token.lexema = token.lexema.Replace("base.", "super.");
            if(this.returnType != null && this.returnType.is_static){
                output.WriteString($"GeneratedCode.{this.class_name}.{token.lexema}");
                return;
            }

            if (add_this)
                output.WriteString($"this.");
            if (is_class)
            {
                output.WriteString($"GeneratedCode.{this.class_name}");
            }
            else
                output.WriteString(token.lexema);
        }
    }
}