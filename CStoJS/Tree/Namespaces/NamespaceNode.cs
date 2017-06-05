using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class NamespaceNode
    {
        public IdentifierNode identifier;
        public List<UsingNode> using_array;
        public List<NamespaceNode> namespace_array;
        public List<TypeDeclarationNode> types_declaration_array;
        public int parent_position;

        public NamespaceNode(){
            this.parent_position = -1;
            this.using_array = new List<UsingNode>();
            this.namespace_array = new List<NamespaceNode>();
            this.types_declaration_array = new List<TypeDeclarationNode>();
            this.identifier = new IdentifierNode();
        }

        public override string ToString(){
            return identifier.ToString();
        }
    }
}