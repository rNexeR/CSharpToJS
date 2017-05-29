using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class PreOperatorExpressionNode : UnaryExpressionNode
    {
        public Token operador;
        public ExpressionNode expression;

        public PreOperatorExpressionNode(Token operador, ExpressionNode expr)
        {
            this.operador = operador;
            this.expression = expr;
        }
    }
}