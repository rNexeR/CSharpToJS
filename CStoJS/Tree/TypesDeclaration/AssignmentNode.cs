using System.Collections.Generic;

namespace CStoJS.Tree
{
    public class AssignmentNode
    {
        public ExpressionNode expression;
        // public List<RowAssignment> inline_assignment;

        public AssignmentNode(){
            // this.inline_assignment = new List<RowAssignment>();
        }

        // public AssignmentNode(ExpressionNode expression, List<RowAssignment> inline_assignment){
        //     this.expression = expression;
        //     this.inline_assignment = inline_assignment;
        // }

        public AssignmentNode(ExpressionNode expression){
            this.expression = expression;
        }
    }
}