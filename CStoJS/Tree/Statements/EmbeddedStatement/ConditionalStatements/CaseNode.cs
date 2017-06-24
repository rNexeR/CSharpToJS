using System;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class CaseNode
    {
        public Token label;
        public ExpressionNode value;

        public CaseNode()
        {

        }

        public CaseNode(Token label, ExpressionNode value)
        {
            this.label = label;
            this.value = value;
        }

        public void EvaluateSemantic(API api, ContextManager context_manager, TypeDeclarationNode eval_type)
        {
            if (label.type == TokenType.CASE_KEYWORD)
            {
                var expr_type = value.EvaluateType(api, context_manager);
                if (expr_type.ToString() != eval_type.ToString()){
                    throw new SemanticException($"Case expression ({expr_type}) mistmatch in type with switch expression ({eval_type}).", this.label);
                }

            }
        }
    }
}