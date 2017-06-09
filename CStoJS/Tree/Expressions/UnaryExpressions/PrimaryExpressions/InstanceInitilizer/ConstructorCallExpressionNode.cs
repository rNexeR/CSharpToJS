using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ConstructorCallExpressionNode : InstanceInitilizerExpressionNode
    {
        public List<ArgumentNode> args;
        public TypeDeclarationNode type;

        public ConstructorCallExpressionNode(TypeDeclarationNode type, List<ArgumentNode> args)
        {
            this.type = type;
            this.args = args;
        }

        public ConstructorCallExpressionNode(){
            
        }
    }
}