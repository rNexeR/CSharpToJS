using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public abstract class BinaryExpressionNode : ExpressionNode
    {
        public ExpressionNode left, right;
        public TypeDeclarationNode l_type, r_type;
        public Token operador;
        protected Dictionary<string, TypeDeclarationNode> rules = new Dictionary<string, TypeDeclarationNode>();
        protected bool SameTypeValid = false;

        public BinaryExpressionNode()
        {

        }

        public BinaryExpressionNode(ExpressionNode left, Token operador, ExpressionNode right)
        {
            this.left = left;
            this.operador = operador;
            this.right = right;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            var leftType = this.left.EvaluateType(api, ctx_man);
            var rightType = this.right.EvaluateType(api, ctx_man);

            if (rightType.ToString() == "Student" && leftType.ToString() == "Person")
                Console.WriteLine("");

            if (SameTypeValid && (Utils.AreEquivalentsTypes(leftType, rightType, ctx_man.GetCurrentNamespaceUsings(), api) || leftType.ToString() == rightType.ToString()))
            {
                this.l_type = leftType;
                this.r_type = rightType;
                this.returnType = leftType;
                return returnType;
            }

            if (!this.rules.ContainsKey($"{leftType},{rightType}"))
                throw new SemanticException($"Rule {leftType},{rightType} not Supported in operator {this.operador}", operador);

            this.returnType = rules[$"{leftType},{rightType}"];

            this.l_type = leftType;
            this.r_type = rightType;

            return returnType;
        }

        public override void GenerateCode(Outputs.IOutput output, API api)
        {
            if (!Utils.assignment_operators.Contains(this.operador.type))
            {
                output.WriteString("(");
                if(this.returnType.ToString() == "IntType")
                    output.WriteString("ToIntPrecision(");
            }
            if (l_type.ToString() == "CharType" && r_type.ToString() == "IntType")
            {
                output.WriteString("CharToInt(");
                left.GenerateCode(output, api);
                output.WriteString(")");
                output.WriteString($" {operador.lexema} ");
                output.WriteString("ToIntPrecision(");
                right.GenerateCode(output, api);
                output.WriteString(")");
            }
            else if (r_type.ToString() == "CharType" && l_type.ToString() == "IntType")
            {
                output.WriteString("ToIntPrecision(");
                left.GenerateCode(output, api);
                output.WriteString(")");
                output.WriteString($" {operador.lexema} ");
                output.WriteString("CharToInt(");
                right.GenerateCode(output, api);
                output.WriteString(")");
            }
            else if (l_type.ToString() == "CharType" && r_type.ToString() == "CharType")
            {
                output.WriteString("CharToInt(");
                left.GenerateCode(output, api);
                output.WriteString(")");
                output.WriteString($" {operador.lexema} ");
                output.WriteString("CharToInt(");
                right.GenerateCode(output, api);
                output.WriteString(")");
            }
            else
            {
                left.GenerateCode(output, api);
                output.WriteString($" {operador.lexema} ");
                right.GenerateCode(output, api);
            }
            if (!Utils.assignment_operators.Contains(this.operador.type))
            {
                output.WriteString(")");
                if(this.returnType.ToString() == "IntType")
                    output.WriteString(")");
            }
        }
    }
}