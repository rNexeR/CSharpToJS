using System;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ConditionalIsExpressionNode: ExpressionNode
    {
        public ExpressionNode left;
        public Token operador;
        public TypeDeclarationNode type;
        public ConditionalIsExpressionNode() : base()
        {

        }

        public ConditionalIsExpressionNode(ExpressionNode left, Token operador, TypeDeclarationNode right)
        {
            this.left = left;
            this.operador = operador;
            this.type = right;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            return new BoolType();
        }
    }
}