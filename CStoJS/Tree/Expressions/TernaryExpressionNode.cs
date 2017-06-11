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

        // public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man){
        //     var trueExp = this.trueExpression.EvaluateType(api, ctx_man);
        //     var falseExp = this.falseExpression.EvaluateType(api, ctx_man);

        //     if(trueExp.identifier.ToString() != falseExp.identifier.ToString())
        //         throw new SemanticException("Ternary Expression: True Expression and False Expression must be of the same Type.", trueExp.identifier.identifiers[0]);
        //     return trueExp;
        // }
    }
}