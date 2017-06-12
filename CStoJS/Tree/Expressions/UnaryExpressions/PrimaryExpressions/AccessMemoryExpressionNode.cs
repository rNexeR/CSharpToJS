using System;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class AccessMemoryExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode identifier;

        public AccessMemoryExpressionNode(ExpressionNode token)
        {
            this.identifier = token;
        }

        public AccessMemoryExpressionNode(){
            
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            return null;
        }
    }
}