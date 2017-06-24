using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public abstract class CastingExpressionNode : UnaryExpressionNode
    {
        public TypeDeclarationNode targetType;
        public ExpressionNode expression;
        public Dictionary<string, TypeDeclarationNode> rules;

        public CastingExpressionNode() : base()
        {
            this.rules = new Dictionary<string, TypeDeclarationNode>();
        }

        public CastingExpressionNode(TypeDeclarationNode targetType, ExpressionNode expression) : this()
        {
            this.targetType = targetType;
            this.expression = expression;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            var expr_type = expression.EvaluateType(api, class_ctx_man);
            var nsp_using = class_ctx_man.GetCurrentNamespaceUsings();
            var expr_type_name = Utils.GetClassName(expr_type.identifier.ToString(), nsp_using, api);
            var target_type_name = Utils.GetClassName(targetType.identifier.ToString(), nsp_using, api);
            if (this.rules.ContainsKey($"{expr_type},{this.targetType}"))
            {
                return this.rules[$"{this.targetType},{expr_type}"];
            }
            else if (expr_type is NullType && (targetType is ClassNode || targetType is StringType)) { }
            else if (expr_type.ToString() == targetType.ToString()) { }
            else if (!Utils.IsChildOf(target_type_name, expr_type_name, api) && !Utils.IsChildOf(expr_type_name, target_type_name, api))
            {
                throw new SemanticException($"Cannot convert {expr_type_name} to {target_type_name}");
            }

            var _usings = class_ctx_man.GetCurrentNamespaceUsings();
            var type_name = Utils.GetClassName(targetType.ToString(), _usings, api);

            return api.GetTypeDeclaration(type_name);
        }
    }
}