using System;

namespace CStoJS.Tree
{
    public class AsExpressionNode : CastingExpressionNode
    {
        public AsExpressionNode() : base()
        {
            
        }

        public AsExpressionNode(TypeDeclarationNode targetType, ExpressionNode expression) : base(targetType, expression)
        {
            
        }
    }
}