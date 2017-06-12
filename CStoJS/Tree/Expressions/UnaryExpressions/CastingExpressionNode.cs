using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeDeclarationNode targetType;
        public ExpressionNode expression;

        public CastingExpressionNode()
        {

        }

        public CastingExpressionNode(TypeDeclarationNode targetType, ExpressionNode expression)
        {
            this.targetType = targetType;
            this.expression = expression;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            var expr_type = expression.EvaluateType(api, ctx_man);
            var nsp_using = ctx_man.GetCurrentNamespaceUsings();
            var target_type_name = Utils.GetClassName(targetType.identifier.ToString(), nsp_using, api);
            if (!Utils.IsChildOf(target_type_name, expr_type.ToString(), api))
            {
                throw new SemanticException($"Cannot convert {expr_type} to {target_type_name}");
            }

            var _usings = ctx_man.GetCurrentNamespaceUsings();
            var type_name = Utils.GetClassName(targetType.ToString(), _usings, api);

            return api.GetTypeDeclaration(type_name);
        }
    }
}