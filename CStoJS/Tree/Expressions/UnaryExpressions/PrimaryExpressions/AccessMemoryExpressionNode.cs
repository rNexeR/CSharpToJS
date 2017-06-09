using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class AccessMemoryExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode identifier;

        public AccessMemoryExpressionNode(ExpressionNode token)
        {
            this.identifier = token;
        }

        public AccessMemoryExpressionNode(){
            
        }
    }
}