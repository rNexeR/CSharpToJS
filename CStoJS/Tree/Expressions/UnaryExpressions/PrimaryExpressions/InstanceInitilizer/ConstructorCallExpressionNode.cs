using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ConstructorCallExpressionNode : InstanceInitilizerExpressionNode
    {
        private List<ArgumentNode> args;
        private TypeDeclarationNode type;

        public ConstructorCallExpressionNode(TypeDeclarationNode type, List<ArgumentNode> args)
        {
            this.type = type;
            this.args = args;
        }
    }
}