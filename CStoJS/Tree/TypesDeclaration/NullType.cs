using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class NullType : TypeDeclarationNode
    {
        public NullType(): base(){

        }

        public NullType(Token identifier){
            identifier.lexema = "NullType";
            this.identifier = new IdentifierNode(identifier);
        }

        public override string ToString(){
            return "NullType";
        }
    }
}