using System;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class NullExpressionNode : UnaryExpressionNode
    {
        public NullExpressionNode() : base() { }
        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            return new NullType();
        }
    }
}