using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public abstract class ArithmeticAssignationExpressionNode : AssignationExpressionNode
    {
        public ArithmeticAssignationExpressionNode() : base() { }
        public ArithmeticAssignationExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right) { }
    }
}