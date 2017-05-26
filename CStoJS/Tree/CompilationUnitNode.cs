using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class CompilationUnitNode
    {
        public List<UsingNode> using_nodes { get; set; }
        public List<NamespaceNode> namespace_node { get; set; }
        public List<TypeDeclarationNode> types_declaration_node { get; set; }

        public CompilationUnitNode()
        {
            this.using_nodes = new List<UsingNode>();
            this.namespace_node = new List<NamespaceNode>();
            this.types_declaration_node = new List<TypeDeclarationNode>();
        }
    }
}