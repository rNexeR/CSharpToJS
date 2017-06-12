using System;
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
            return null;
        }
    }
}