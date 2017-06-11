namespace CStoJS.Tree
{
    public class FloatType : TypeDeclarationNode
    {
        public FloatType(){
            this.type = "float";
        }

        public FloatType(IdentifierNode identifier) : this(){
            this.identifier = identifier;
        }

        public override string ToString(){
            return "FloatType";
        }
    }
}