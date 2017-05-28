using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class IdentifierTypeNode : TypeDeclarationNode
    {
        public IdentifierNode typeIdentifier;
        public IdentifierTypeNode() : base(){
            this.type = "UserDefined";
        }

        public IdentifierTypeNode(IdentifierNode typeIdentifier) : this(){
            this.typeIdentifier = typeIdentifier;
        }
    }
}