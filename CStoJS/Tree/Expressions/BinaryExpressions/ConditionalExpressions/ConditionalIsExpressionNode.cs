using System;
using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ConditionalIsExpressionNode: ExpressionNode
    {
        public ExpressionNode left;
        public Token operador;
        public TypeDeclarationNode type;
        public ConditionalIsExpressionNode()
        {

        }

        public ConditionalIsExpressionNode(ExpressionNode left, Token operador, TypeDeclarationNode right)
        {
            this.left = left;
            this.operador = operador;
            this.type = right;
        }

        // public override TypeDeclarationNode EvaluateType()
        // {
        //     return new BoolType();
        // }
    }
}