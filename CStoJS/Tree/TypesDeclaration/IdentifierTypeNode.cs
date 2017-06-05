using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class IdentifierTypeNode : TypeDeclarationNode
    {
        public IdentifierTypeNode() : base(){
            this.type = "UserDefined";
        }

        public IdentifierTypeNode(IdentifierNode typeIdentifier) : this(){
            this.identifier = typeIdentifier;
        }
    }
}