using System.Collections.Generic;
using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class InterfaceNode : TypeDeclarationNode
    {
        public List<IdentifierNode> inherit;
        public List<MethodNode> methods;
        public InterfaceNode(){
            this.type = "interface";
            this.inherit = new List<IdentifierNode>();
            this.methods = new List<MethodNode>();
        }

        public InterfaceNode(IdentifierNode identifier,  List<IdentifierNode> inherit, List<MethodNode> methods) : this(){
            this.inherit = inherit;
            this.methods = methods;
            this.identifier = identifier;
        }

        public void AddBase(IdentifierNode identifier){
            this.inherit.Add(identifier);
        }

        public void AddMethod(MethodNode method){
            this.methods.Add(method);
        }
    }
}