using System.Collections.Generic;
using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class IdentifierNode
    {
        public List<Token> identifiers {get; set;}
        public IdentifierNode(){
            this.identifiers = new List<Token>();
        }

        public IdentifierNode(List<Token> identifiers){
            this.identifiers = identifiers;
        }

        public IdentifierNode(Token identifier) : this(){
            this.identifiers.Add(identifier);

        }

        public override string ToString(){
            var identifiers_strings = new List<string>();
            foreach(var x in identifiers){
                identifiers_strings.Add(x.lexema);
            }
            return string.Join(".", identifiers_strings);
        }
    }
}