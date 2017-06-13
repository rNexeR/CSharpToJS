using System;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class PlusEqualAssignationExpressionNode : ArithmeticAssignationExpressionNode
    {
        public PlusEqualAssignationExpressionNode() : base() { this.InitializeRules(); }
        public PlusEqualAssignationExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right) { this.InitializeRules(); }
        public void InitializeRules()
        {
            this.rules["IntType,IntType"] = new IntType();
            this.rules["FloatType,FloatType"] = new FloatType();
            this.rules["FloatType,CharType"] = new FloatType();
            this.rules["FloatType,IntType"] = new FloatType();
            this.rules["IntType,CharType"] = new IntType();
            this.rules["CharType,CharType"] = new IntType();

            this.rules["StringType,FloatType"] = new StringType();
            this.rules["StringType,IntType"] = new StringType();
            this.rules["StringType,CharType"] = new StringType();
            this.rules["StringType,StringType"] = new StringType();
        }
    }
}