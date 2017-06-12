using System;
using System.Collections.Generic;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class InlineExpressionNode : UnaryExpressionNode
    {
        public List<ExpressionNode> expressions;

        public InlineExpressionNode() : base()
        {

        }

        public InlineExpressionNode(List<ExpressionNode> expression)
        {
            this.expressions = expression;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            throw new NotImplementedException();
        }
    }
}