using System.Collections.Generic;


namespace CStoJS.Tree
{
    public class EnumDefinitionNode : TypeDeclarationNode
    {
        
        public List<EnumNode> enum_node {get; set;}

        public EnumDefinitionNode(){
            this.type = "enum";
            this.enum_node = new List<EnumNode>();
        }
    }
}