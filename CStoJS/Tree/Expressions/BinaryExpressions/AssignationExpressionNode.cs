using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class AssignationExpressionNode : BinaryExpressionNode
    {
        public AssignationExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }

        public AssignationExpressionNode(){
            
        }
    }
}