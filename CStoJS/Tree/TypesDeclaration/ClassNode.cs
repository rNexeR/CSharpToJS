using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ClassNode : TypeDeclarationNode
    {
        public List<IdentifierNode> inherit;
        public List<MethodNode> methods;
        public List<FieldNode> fields;
        public List<ConstructorNode> constructors;
        public bool isAbstract; 
        public ClassNode(){
            this.type = "class";
            this.inherit = new List<IdentifierNode>();
            this.methods = new List<MethodNode>();
            this.fields = new List<FieldNode>();
            this.constructors = new List<ConstructorNode>();
            this.isAbstract = false;
        }
    }
}