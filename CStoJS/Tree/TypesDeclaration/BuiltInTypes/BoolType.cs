namespace CStoJS.Tree
{
    public class BoolType : TypeDeclarationNode
    {
        public BoolType(){
            this.type = "char";
        }
        public BoolType(IdentifierNode identifier) : this(){
            this.identifier = identifier;
        }

        public override string ToString(){
            return "BoolType";
        }
    }
}