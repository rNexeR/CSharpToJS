using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class PreOperatorExpressionNode : UnaryExpressionNode
    {
        public Token operador;
        public ExpressionNode expression;

        public PreOperatorExpressionNode()
        {

        }

        public PreOperatorExpressionNode(Token operador, ExpressionNode expr)
        {
            this.operador = operador;
            this.expression = expr;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man)
        {
            var expr_type = expression.EvaluateType(api, class_ctx_man);
            var temp1 = new List<TokenType> { TokenType.OP_SUBSTRACT, TokenType.OP_SUM, TokenType.OP_INC_MM, TokenType.OP_INC_PP };
            if (temp1.Contains(operador.type))
            {
                var match = new List<string> { "CharType", "IntType", "FloatType" };
                if (!match.Contains(expr_type.ToString()))
                    throw new SemanticException($"Operator {this.operador.lexema} can only be applied to Char, Int And Float types.", operador);
            }
            else if (operador.type == TokenType.OP_BITS_COMPLEMENT)
            {
                var match = new List<string> { "CharType", "IntType" };
                if(!(match.Contains(expr_type.ToString())))
                    throw new SemanticException($"Operator {this.operador.lexema} can only be applied to Bitwise Expressions.", operador);
                return new IntType();
            }
            else
            {
                if(expr_type.ToString() != "BoolType")
                    throw new SemanticException($"Operator {this.operador.lexema} can only be applied to Boolean Expressions.", operador);
            }
            return expr_type;
        }
    }
}