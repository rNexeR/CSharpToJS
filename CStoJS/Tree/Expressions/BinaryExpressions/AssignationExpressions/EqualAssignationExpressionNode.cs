using System;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class EqualAssignationExpressionNode : AssignationExpressionNode
    {
        public EqualAssignationExpressionNode() : base()
        {
            this.InitializeRules();
        }
        public EqualAssignationExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
            this.InitializeRules();
        }

        public void InitializeRules()
        {
            this.SameTypeValid = true;
            this.rules["FloatType,IntType"] = new FloatType();
            this.rules["IntType,CharType"] = new IntType();
        }
    }
}