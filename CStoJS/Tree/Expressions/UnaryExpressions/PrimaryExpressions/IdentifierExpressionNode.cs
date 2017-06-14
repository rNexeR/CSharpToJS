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

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            // var _usings = ctx_man.GetCurrentNamespaceUsings();
            // var class_name = Utils.GetClassName(token.lexema, _usings, api);
            if(!ctx_man.VariableExists(token.lexema) /*&& api.GetTypeDeclaration(class_name) == null*/)
                throw new SemanticException($"Field {token.lexema} doesn't exist in type {ctx_man.GetCurrentClass()} or is inaccesible due to its protection lever or is declared as static", token);
            
            return ctx_man.GetVariableType(token.lexema);
        }
    }
}