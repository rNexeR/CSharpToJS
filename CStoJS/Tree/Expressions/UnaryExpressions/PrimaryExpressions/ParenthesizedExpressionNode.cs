using System;
using System.Collections.Generic;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ParenthesizedExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode expressionNode;

        public ParenthesizedExpressionNode(ExpressionNode expressionNode)
        {
            this.expressionNode = expressionNode;
        }

        public ParenthesizedExpressionNode(){
            
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            return expressionNode.EvaluateType(api, ctx_man);
        }
    }
}