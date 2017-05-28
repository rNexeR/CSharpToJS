namespace CStoJS.Tree
{
    public class RowAssignment
    {
        public IdentifierNode leftOperand;
        public ExpressionNode rightOperand;

        public RowAssignment(){
            
        }

        public RowAssignment(IdentifierNode identifier, ExpressionNode expression){
            this.leftOperand = identifier;
            this.rightOperand = expression;
        }
    }
}