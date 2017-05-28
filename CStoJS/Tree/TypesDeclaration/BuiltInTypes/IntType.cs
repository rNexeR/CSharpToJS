namespace CStoJS.Tree
{
    public class IntType : TypeDeclarationNode
    {
        public IntType(){
            this.type = "int";
        }
        public IntType(IdentifierNode identifier) : this(){
            this.identifier = identifier;
        }
    }
}