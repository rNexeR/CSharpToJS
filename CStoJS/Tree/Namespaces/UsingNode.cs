using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class UsingNode
    {
        public IdentifierNode identifier {get; set;}

        public UsingNode(IdentifierNode identifier){
            this.identifier = identifier;
        }

        public UsingNode(){
            
        }

        public override string ToString(){
            return identifier.ToString();
        }
    }
}