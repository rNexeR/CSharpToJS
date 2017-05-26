using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class NamespaceNode
    {
        public IdentifierNode identifier;
        public List<UsingNode> using_array {get; set;}
        public List<NamespaceNode> namespace_array {get; set;}
        public List<TypeDeclarationNode> types_declaration_array {get; set;}

        public NamespaceNode(){
            this.using_array = new List<UsingNode>();
            this.namespace_array = new List<NamespaceNode>();
            this.types_declaration_array = new List<TypeDeclarationNode>();
        }

        public override string ToString(){
            return identifier.ToString();
        }
    }
}