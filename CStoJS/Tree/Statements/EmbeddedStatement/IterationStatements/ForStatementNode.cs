using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class ForStatementNode : EmbeddedStatementNode
    {
        public List<StatementNode> initializerExpression;
        public ExpressionNode conditionalExpression;
        public List<StatementNode> incrementExpression;
        public EmbeddedStatementNode body;

        public ForStatementNode(){
            this.initializerExpression = new List<StatementNode>();
        }

        public ForStatementNode(List<StatementNode> initializer, ExpressionNode conditional, List<StatementNode> increment, EmbeddedStatementNode body){
            this.initializerExpression = initializer;
            this.conditionalExpression = conditional;
            this.incrementExpression = increment;
            this.body = body;
        }
    }
}