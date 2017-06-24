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

        public BinaryExpressionNode(){

        }

        public BinaryExpressionNode(ExpressionNode left, Token operador, ExpressionNode right){
            this.left = left;
            this.operador = operador;
            this.right = right;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man){
            var leftType = this.left.EvaluateType(api, ctx_man);
            var rightType = this.right.EvaluateType(api, ctx_man);

            if(SameTypeValid && Utils.AreEquivalentsTypes(leftType, rightType, ctx_man.GetCurrentNamespaceUsings(), api)){
                return leftType;
            }

            if(!this.rules.ContainsKey($"{leftType},{rightType}"))
                throw new SemanticException("Rule not Supported");

            this.returnType = rules[$"{leftType},{rightType}"];

            this.l_type = leftType;
            this.r_type = rightType;

            return returnType;
        }
    }
}