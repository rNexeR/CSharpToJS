using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ExpressionUnaryNode : UnaryExpressionNode
    {
        public Token unaryOperator;
        public ExpressionNode expression;

        public ExpressionUnaryNode(){

        }

        public ExpressionUnaryNode(Token unaryOperator, ExpressionNode expression){
            this.expression = expression;
            this.unaryOperator = unaryOperator;
        }
    }
}