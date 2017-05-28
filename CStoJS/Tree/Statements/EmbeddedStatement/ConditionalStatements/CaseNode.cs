using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class CaseNode
    {
        public Token label;
        public ExpressionNode value;

        public CaseNode(){

        }

        public CaseNode(Token label, ExpressionNode value){
            this.label = label;
            this.value = value;
        }
    }
}