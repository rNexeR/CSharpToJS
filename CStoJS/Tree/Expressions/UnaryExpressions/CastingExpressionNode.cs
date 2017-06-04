using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeDeclarationNode targetType;
        public List<ExpressionNode> expression;

        public CastingExpressionNode(){

        }

        public CastingExpressionNode(TypeDeclarationNode targetType, List<ExpressionNode> expression){
            this.targetType = targetType;
            this.expression = expression;
        }
    }
}