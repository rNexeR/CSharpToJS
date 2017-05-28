namespace CStoJS.Tree
{
    public class VarType : TypeDeclarationNode
    {
        public VarType()
        {
            this.type = "void";
        }

        public VarType(IdentifierNode identifier) : this()
        {
            this.identifier = identifier;
        }
    }
}