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

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            return expressionNode.EvaluateType(api, class_ctx_man);
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            output.WriteString("(");
            expressionNode.GenerateCode(output, api);
            output.WriteString(")");
        }
    }
}