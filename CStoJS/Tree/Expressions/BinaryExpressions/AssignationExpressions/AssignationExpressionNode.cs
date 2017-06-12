using System;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public abstract class AssignationExpressionNode : BinaryExpressionNode
    {
        public AssignationExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }

        public AssignationExpressionNode(){
        }

        // public override TypeDeclarationNode EvaluateType()
        // {
        //     var leftType = this.left.EvaluateType();
        //     var rightType = this.right.EvaluateType();

        //     if(leftType.identifier.ToString() != rightType.identifier.ToString())
        //         throw new SemanticException($"AssignationExpression: cannot assign {rightType.identifier.ToString()} to {leftType.identifier.ToString()}.", leftType.identifier.identifiers[0]);
            
        //     return leftType;
        // }
    }
}