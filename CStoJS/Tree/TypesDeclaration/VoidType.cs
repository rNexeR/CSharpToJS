namespace CStoJS.Tree
{
    public class VoidType : TypeDeclarationNode
    {
        public VoidType()
        {
            this.type = "void";
        }

        public VoidType(IdentifierNode identifier) : this()
        {
            this.identifier = identifier;
        }

        public override string ToString(){
            return "VoidType";
        }
    }
}