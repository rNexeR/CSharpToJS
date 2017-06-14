using System;

namespace CStoJS.Tree
{
    public class ParenthisizedCastingExpressionNode : CastingExpressionNode
    {
        public ParenthisizedCastingExpressionNode() : base()
        {
            this.InitializeRules();
        }

        public ParenthisizedCastingExpressionNode(TypeDeclarationNode targetType, ExpressionNode expression) : base(targetType, expression)
        {
            this.InitializeRules();
        }

        private void InitializeRules()
        {
            this.rules["IntType,FloatType"] = new IntType();
            this.rules["FloatType,IntType"] = new FloatType();
            this.rules["CharType,IntType"] = new CharType();
            this.rules["IntType,CharType"] = new IntType();
        }
    }
}