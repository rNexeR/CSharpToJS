using System.Collections.Generic;

namespace CStoJS.Tree
{
    /*
    type-declaration-node:
	-> class-node
	-> enum-node
	-> interface-node
     */
    public abstract class TypeDeclarationNode
    {
        public EncapsulationNode encapsulation_modifier;
        public IdentifierNode identifier;
        public string type;

        public TypeDeclarationNode(){
            this.encapsulation_modifier = new EncapsulationNode();
            this.identifier = new IdentifierNode();
        }

        public override string ToString(){
            return $"{encapsulation_modifier} {type} {identifier}";
        }
    }
}