using System;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ModAssignationExpressionNode : ArithmeticAssignationExpressionNode
    {
        public ModAssignationExpressionNode() : base() { this.InitializeRules(); }
        public ModAssignationExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right) { this.InitializeRules(); }
        public void InitializeRules()
        {
            this.rules["IntType,IntType"] = new IntType();
            this.rules["FloatType,FloatType"] = new FloatType();
            this.rules["FloatType,CharType"] = new FloatType();
            this.rules["FloatType,IntType"] = new FloatType();
            this.rules["IntType,CharType"] = new IntType();
            this.rules["CharType,CharType"] = new IntType();
        }
    }
}