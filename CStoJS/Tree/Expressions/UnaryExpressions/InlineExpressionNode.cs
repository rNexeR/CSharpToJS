using System;
using System.Collections.Generic;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class InlineExpressionNode : UnaryExpressionNode
    {
        public List<ExpressionNode> expressions;

        public InlineExpressionNode() : base()
        {

        }

        public InlineExpressionNode(List<ExpressionNode> expression)
        {
            this.expressions = expression;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man, ContextManager st_ctx_man = null)
        {
            var ctx_man_cpy = new ContextManager(api);
            var _usings = ctx_man.GetCurrentNamespaceUsings();
            TypeDeclarationNode ret_type = null;
            if (expressions[0] is IdentifierExpressionNode && ((expressions[0] as IdentifierExpressionNode).token.lexema == "this" || (expressions[0] as IdentifierExpressionNode).token.lexema == "base"))
            {
                expressions.RemoveAt(0);

            }

            var i = 0;
            foreach (var expr in expressions)
            {
                if (i == 0)
                {
                    ret_type = expr is UnaryExpressionNode ? (expr as UnaryExpressionNode).EvaluateType(api, ctx_man, st_ctx_man) : expr.EvaluateType(api, ctx_man);
                    var current_ctx = Utils.GetClassName(ret_type.ToString(), _usings, api);
                    var add_private_members = true;
                    var ret_type_name = Utils.GetClassName(ret_type.identifier.ToString(), _usings, api);
                    if ((expr is InstanceInitilizerExpressionNode) || (!Utils.IsChildOf(ctx_man.GetCurrentClass(), ret_type_name, api) && !Utils.IsChildOf(ret_type_name, ctx_man.GetCurrentClass(), api) ) )
                        add_private_members = false;
                    ctx_man_cpy.Push(new Context(ContextType.CLASS_CONTEXT, current_ctx), current_ctx, add_private_members);
                    if (
                        (expr is IdentifierExpressionNode && (expr as IdentifierExpressionNode).token.lexema == ret_type.identifier.ToString())
                    // || ctx_man.IsStaticContext()
                    )
                        ctx_man_cpy.SetStaticContext();
                }
                else
                {
                    ret_type = expr is UnaryExpressionNode ? (expr as UnaryExpressionNode).EvaluateType(api, ctx_man, ctx_man_cpy) : expr.EvaluateType(api, ctx_man);
                    var current_ctx = Utils.GetClassName(ret_type.ToString(), _usings, api);
                    ctx_man_cpy.Clear();
                    var add_private_members = true;
                    if (expr is InstanceInitilizerExpressionNode)
                        add_private_members = false;
                    ctx_man_cpy.Push(new Context(ContextType.CLASS_CONTEXT, current_ctx), current_ctx, add_private_members);
                }
                i++;
            }
            return ret_type;
        }
    }
}