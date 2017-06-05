using System.Collections.Generic;
using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ArrayAccessExpressionNode : PrimaryExpressionNode
    {
        public Token identifier;
        public List<ArrayAccessNode> indexes;

        public ArrayAccessExpressionNode(List<ArrayAccessNode> indexes)
        {
            this.indexes = indexes;
        }

        public ArrayAccessExpressionNode(){
            
        }
    }
}