using CStoJS.Exceptions;

namespace CStoJS.Tree
{
    public class TernaryExpressionNode : ExpressionNode
    {
        public ExpressionNode conditionalExpression;
        public ExpressionNode trueExpression;
        public ExpressionNode falseExpression;

        public TernaryExpressionNode() : base(){

        }

        public TernaryExpressionNode(ExpressionNode before, ExpressionNode truth, ExpressionNode lie)
        {
            this.conditionalExpression = before;
            this.trueExpression = truth;
            this.falseExpression = lie;
        }

        public override TypeDeclarationNode EvaluateType(){
            var trueExp = this.trueExpression.EvaluateType();
            var falseExp = this.falseExpression.EvaluateType();

            if(trueExp.identifier.ToString() != falseExp.identifier.ToString())
                throw new SemanticException("Ternary Expression: trueExpression and False expression must be of the same Type.", trueExp.identifier.identifiers[0]);
            return trueExp;
        }
    }
}