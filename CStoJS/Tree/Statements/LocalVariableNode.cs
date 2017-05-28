namespace CStoJS.Tree
{
    public class LocalVariableNode : StatementNode
    {
        public TypeDeclarationNode type;
        public IdentifierNode identifier;
        public ExpressionNode assignation;
        public LocalVariableNode(){
            
        }

        public LocalVariableNode(TypeDeclarationNode type, IdentifierNode identifier){
            this.identifier = identifier;
            this.type = type;
        }

        public LocalVariableNode(TypeDeclarationNode type, IdentifierNode identifier, ExpressionNode assignation) : this(type, identifier){
            this.assignation = assignation;
        }
    }
}