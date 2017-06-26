using System;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class TernaryExpressionNode : ExpressionNode
    {
        public ExpressionNode conditionalExpression;
        public ExpressionNode trueExpression;
        public ExpressionNode falseExpression;

        public TernaryExpressionNode() : base(){

        }

        public TernaryExpressionNode(ExpressionNode before, ExpressionNode true_expr, ExpressionNode false_expr)
        {
            this.conditionalExpression = before;
            this.trueExpression = true_expr;
            this.falseExpression = false_expr;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man){
            var cond_type = this.conditionalExpression.EvaluateType(api, ctx_man);

            if(cond_type.ToString() != "BoolType")
                throw new SemanticException("Conditional Expression doesn't return a BoolType.");

            var trueExp = this.trueExpression.EvaluateType(api, ctx_man);
            var falseExp = this.falseExpression.EvaluateType(api, ctx_man);

            if(trueExp.ToString() != falseExp.ToString())
                throw new SemanticException("Ternary Expression: True Expression and False Expression must be of the same Type.");
            return trueExp;
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            conditionalExpression.GenerateCode(output, api);
            output.WriteString(" ? ");
            trueExpression.GenerateCode(output, api);
            output.WriteString(" : ");
            falseExpression.GenerateCode(output, api);
        }
    }
}