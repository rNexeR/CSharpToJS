using System;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class IdentifierExpressionNode : PrimaryExpressionNode
    {
        public Token token;

        public IdentifierExpressionNode() : base(){

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
            var is_class = false;
            var class_name = Utils.GetClassName(this.token.lexema, usings, api);
            if(class_name != "")
                is_class = true;
            if(!ctx_man.VariableExists(token.lexema) && !is_class /*&& api.GetTypeDeclaration(class_name) == null*/)
                if(!ctx_man.IsStaticContext())
                    throw new SemanticException($"Variable {token.lexema} doesn't exists in type({ctx_man.GetCurrentClass()}), is inaccesible due to its protection lever or is declared as static", token);
                else
                    throw new SemanticException($"Variable {token.lexema} doesn't exists in type({ctx_man.GetCurrentClass()}) or is not declared as static", token);
            
            if(is_class)
                return api.GetTypeDeclaration(class_name);

            return ctx_man.GetVariableType(token.lexema);
        }
    }
}