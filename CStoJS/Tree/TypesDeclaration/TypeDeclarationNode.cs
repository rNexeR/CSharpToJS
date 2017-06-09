using System;
using System.Collections.Generic;
using CStoJS.Semantic;

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
        public int namespace_index;

        public TypeDeclarationNode(){
            this.encapsulation_modifier = new EncapsulationNode();
            this.identifier = new IdentifierNode();
        }

        public override string ToString(){
            return $"{identifier}";
        }

        public virtual void EvaluateSemantic(API api){
            Console.WriteLine($"Evaluating {identifier} from TypeDeclarationNode");
        }
    }
}