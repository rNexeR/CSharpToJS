using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public abstract class BinaryExpressionNode : ExpressionNode
    {
        public ExpressionNode left;
        public Token operador;
        public ExpressionNode right;
        protected Dictionary<string, TypeDeclarationNode> rules;

        public BinaryExpressionNode(){

        }

        public BinaryExpressionNode(ExpressionNode left, Token operador, ExpressionNode right){
            this.left = left;
            this.operador = operador;
            this.right = right;
        }

        public override TypeDeclarationNode EvaluateType(){
            var leftType = this.left.EvaluateType().identifier.ToString();
            var rightType = this.right.EvaluateType().identifier.ToString();

            if(!this.rules.ContainsKey($"{leftType},{rightType}"))
                throw new SemanticException("Rule not Supported", this.left.EvaluateType().identifier.identifiers[0]);

            return rules[$"{leftType},{rightType}"];
        }
    }
}