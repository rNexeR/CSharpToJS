namespace CStoJS.Tree
{
    public class VarType : TypeDeclarationNode
    {
        public VarType()
        {
            this.type = "var";
        }

        public VarType(IdentifierNode identifier) : this()
        {
            this.identifier = identifier;
        }

        public override string ToString(){
            return "VarType";
        }
    }
}