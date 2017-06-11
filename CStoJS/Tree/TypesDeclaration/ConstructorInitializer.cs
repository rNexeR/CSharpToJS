using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ConstructorInitializerNode
    {
        public IdentifierNode identifier;
        public List<ArgumentNode> arguments;

        public ConstructorInitializerNode(){
            this.arguments = new List<ArgumentNode>();
        }

        public ConstructorInitializerNode(IdentifierNode identifier, List<ArgumentNode> args){
            this.identifier = identifier;
            this.arguments = args;
        }
    }
}