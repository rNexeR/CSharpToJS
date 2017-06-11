using System.Collections.Generic;

namespace CStoJS.Tree
{
    public abstract class TypeDefinitionNode : TypeDeclarationNode
    {
        public IdentifierNode namespace_name;
        public List<IdentifierNode> inherit;
        public List<MethodNode> methods;
        public List<FieldNode> fields;
        public List<ConstructorNode> constructors;
        public bool isAbstract; 

        public TypeDefinitionNode(){
            this.inherit = new List<IdentifierNode>();
            this.methods = new List<MethodNode>();
            this.fields = new List<FieldNode>();
            this.constructors = new List<ConstructorNode>();
        }
    }
}