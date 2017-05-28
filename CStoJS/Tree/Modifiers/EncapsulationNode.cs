using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class EncapsulationNode
    {
        public Token token {get; set;}

        public EncapsulationNode(){

        }

        public EncapsulationNode(Token token){
            this.token = token;
        }

        public override string ToString(){
            return token.lexema;
        }
    }
}