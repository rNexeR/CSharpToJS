using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class BuiltInTypeExpressionNode : PrimaryExpressionNode
    {
        private Token token;

        public BuiltInTypeExpressionNode() : base(){

        }

        public BuiltInTypeExpressionNode(Token token)
        {
            this.token = token;
        }
    }
}