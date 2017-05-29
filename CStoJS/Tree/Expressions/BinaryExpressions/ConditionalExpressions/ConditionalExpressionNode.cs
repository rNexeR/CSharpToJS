using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ConditionalExpressionNode : BinaryExpressionNode
    {
        private TypeDeclarationNode type;

        public ConditionalExpressionNode() : base(){
            
        }
        public ConditionalExpressionNode(ExpressionNode left, Token operador, ExpressionNode right) : base(left, operador, right)
        {
        }

        public ConditionalExpressionNode(ExpressionNode left, Token operador, TypeDeclarationNode type)
        {
            this.left = left;
            this.operador = operador;
            this.type = type;
        }
    }
}