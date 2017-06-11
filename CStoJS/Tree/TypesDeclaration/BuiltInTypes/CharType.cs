namespace CStoJS.Tree
{
    public class CharType : TypeDeclarationNode
    {
        public CharType(){
            this.type = "char";
        }
        public CharType(IdentifierNode identifier) : this(){
            this.identifier = identifier;
        }

        public override string ToString(){
            return "CharType";
        }
    }
}