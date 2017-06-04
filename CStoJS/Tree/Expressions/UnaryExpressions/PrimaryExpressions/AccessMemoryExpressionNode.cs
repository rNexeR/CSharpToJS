using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class AccessMemoryExpressionNode : PrimaryExpressionNode
    {
        public Token token;

        public AccessMemoryExpressionNode(Token token)
        {
            this.token = token;
        }

        public AccessMemoryExpressionNode(){
            
        }
    }
}