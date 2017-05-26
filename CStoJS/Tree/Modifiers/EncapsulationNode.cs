using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class EncapsulationNode
    {
        public Token token {get; set;}

        public override string ToString(){
            return token.lexema;
        }
    }
}