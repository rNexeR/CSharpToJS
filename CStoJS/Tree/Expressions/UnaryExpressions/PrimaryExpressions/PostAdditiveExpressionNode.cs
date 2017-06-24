using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class PostAdditiveExpressionNode : PrimaryExpressionNode
    {
        public Token operador;
        public ExpressionNode expression;

        public PostAdditiveExpressionNode(Token token)
        {
            this.operador = token;
        }

        public PostAdditiveExpressionNode()
        {

        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            var expr_type = expression is UnaryExpressionNode ? (expression as UnaryExpressionNode).EvaluateType(api, class_ctx_man, st_ctx_man) : expression.EvaluateType(api, class_ctx_man);
            var match = new List<string> { "CharType", "IntType", "FloatType" };
            if (!match.Contains(expr_type.ToString()))
                throw new SemanticException($"Operator {this.operador.lexema} can only be applied to Char, Int And Float types.", operador);
            return expr_type;
        }
    }
}