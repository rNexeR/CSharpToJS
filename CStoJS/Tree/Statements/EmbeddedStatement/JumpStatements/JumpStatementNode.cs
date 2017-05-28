using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class JumpStatementNode : EmbeddedStatementNode
    {
        public Token identifier;
        public ExpressionNode optionalExpression;

        public JumpStatementNode(){

        }

        public JumpStatementNode(Token identifier, ExpressionNode optionalExpression){
            this.identifier = identifier;
            this.optionalExpression = optionalExpression;
        }
    }
}