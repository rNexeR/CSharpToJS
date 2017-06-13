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

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            var ctx_man_cpy = new ContextManager(api);
            var _usings = ctx_man.GetCurrentNamespaceUsings();
            TypeDeclarationNode ret_type = null;
            var i = 0;
            foreach (var expr in expressions)
            {
                if (i == 0)
                {
                    ret_type = expr.EvaluateType(api, ctx_man);
                    var current_ctx = Utils.GetClassName(ret_type.ToString(), _usings, api);
                    ctx_man_cpy.Push(new Context(ContextType.CLASS_CONTEXT, current_ctx), current_ctx);
                }else{
                    ret_type = expr.EvaluateType(api, ctx_man_cpy);
                    var current_ctx = Utils.GetClassName(ret_type.ToString(), _usings, api);
                    ctx_man_cpy.Clear();
                    ctx_man_cpy.Push(new Context(ContextType.CLASS_CONTEXT, current_ctx), current_ctx);
                }
                i++;
            }
            return ret_type;
        }
    }
}