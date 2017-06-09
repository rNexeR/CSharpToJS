using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class CaseExpressionNode
    {
        public List<CaseNode> toCompareValue;
        public List<StatementNode> body;

        public CaseExpressionNode(){
            this.toCompareValue = new List<CaseNode>();
            this.body = new List<StatementNode>();
        }

        public CaseExpressionNode(List<CaseNode> toCompare, List<StatementNode> body){
            this.toCompareValue = toCompare;
            this.body = body;
        }
    }
}