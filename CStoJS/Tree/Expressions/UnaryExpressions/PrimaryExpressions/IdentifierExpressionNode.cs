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
            if(!ctx_man.VariableExists(token.lexema))
                throw new SemanticException($"Field {token.lexema} doesn't exist in type {ctx_man.GetCurrentClass()}", token);
            return ctx_man.GetVariableType(token.lexema);
        }
    }
}