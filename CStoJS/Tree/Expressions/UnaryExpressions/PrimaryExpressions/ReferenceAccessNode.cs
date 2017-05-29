using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ReferenceAccessNode : PrimaryExpressionNode
    {
        private Token token;

        public ReferenceAccessNode(Token token)
        {
            this.token = token;
        }

        public ReferenceAccessNode(){
            
        }
    }
}