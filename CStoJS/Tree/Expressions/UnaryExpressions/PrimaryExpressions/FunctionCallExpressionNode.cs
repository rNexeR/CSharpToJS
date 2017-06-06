using System.Collections.Generic;
using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode identifier;
        public List<ArgumentNode> args;

        public FunctionCallExpressionNode(List<ArgumentNode> args)
        {
            this.args = args;
        }

        public FunctionCallExpressionNode(){
            
        }
    }
}