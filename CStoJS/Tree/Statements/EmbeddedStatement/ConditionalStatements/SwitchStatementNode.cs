using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class SwitchStatementNode : EmbeddedStatementNode
    {
        public ExpressionNode expressionToEval;
        public List<CaseExpressionNode> casess;

        public SwitchStatementNode(){
            this.casess = new List<CaseExpressionNode>();
        }

        public SwitchStatementNode(ExpressionNode exprToEval, List<CaseExpressionNode> cases){
            this.expressionToEval = exprToEval;
            this.casess = cases;
        }
    }
}