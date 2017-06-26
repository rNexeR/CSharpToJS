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

        public override void GenerateCode(Outputs.IOutput output, Semantic.API api){
            this.expression.GenerateCode(output, api);
        }
    }
}