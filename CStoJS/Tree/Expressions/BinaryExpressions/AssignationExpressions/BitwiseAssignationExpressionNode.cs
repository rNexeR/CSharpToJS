using System;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class BitwiseAssignationExpressionNode : AssignationExpressionNode
    {
        public BitwiseAssignationExpressionNode() : base()
        {
            this.InitializeRules();
        }

        public BitwiseAssignationExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
            this.InitializeRules();
        }

        public void InitializeRules()
        {
            this.rules["IntType,IntType"] = new IntType();
        }
    }
}