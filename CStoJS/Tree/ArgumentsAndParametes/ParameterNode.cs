namespace CStoJS.Tree
{
    public class ParameterNode
    {
        public TypeDeclarationNode type;
        public IdentifierNode identifier;

        public ParameterNode(){

        }

        public ParameterNode(TypeDeclarationNode type, IdentifierNode identifier){
            this.type = type;
            this.identifier = identifier;
        }
    }
}