namespace CStoJS.Tree
{
    public class StringType : TypeDeclarationNode
    {
        public StringType(){
            this.type = "string";
        }
        public StringType(IdentifierNode identifier) : this(){
            this.identifier = identifier;
        }

        public override string ToString(){
            return "StringType";
        }
    }
}