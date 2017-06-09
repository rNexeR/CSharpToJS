using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class LiteralExpressionNode : PrimaryExpressionNode
    {
        public Token literal;

        public LiteralExpressionNode() : base(){
            
        }

        public LiteralExpressionNode(Token literal)
        {
            this.literal = literal;
        }
    }
}