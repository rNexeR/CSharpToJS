using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode identifier;
        public List<ArgumentNode> args;

        private bool add_this = false;

        public FunctionCallExpressionNode(List<ArgumentNode> args)
        {
            this.args = args;
        }

        public FunctionCallExpressionNode()
        {

        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            var id_node = identifier as IdentifierExpressionNode;
            var id_name = id_node.token.lexema;

            var args = new List<string>();
            foreach (var arg in this.args)
            {
                args.Add(arg.argument.EvaluateType(api, class_ctx_man).ToString());
            }
            var fn_name = $"{id_name}({string.Join(",", args)})";
            var ctx_man = st_ctx_man is null ? class_ctx_man : st_ctx_man;
            if (!ctx_man.MethodExists(fn_name))
                if (!ctx_man.IsStaticContext())
                    throw new SemanticException($"Method {fn_name} doesn't exists in type({ctx_man.GetCurrentClass()}), is inaccesible due to its protection lever or is declared as static", id_node.token);
                else
                    throw new SemanticException($"Method {fn_name} doesn't exists in type({ctx_man.GetCurrentClass()}) or is not declared as static", id_node.token);

            var ret = ctx_man.GetMethodReturnType(fn_name);

            this.add_this = ret.is_in_class && st_ctx_man is null && !id_name.StartsWith("this.") && !id_name.StartsWith("base.");

            if(this.add_this && ret.is_static){
                this.add_this = false;
                (this.identifier as IdentifierExpressionNode).token.lexema = "GeneratedCode." + ctx_man.GetCurrentClass() + "." + (this.identifier as IdentifierExpressionNode).token.lexema;
            }

            return ret;
        }

        public override void GenerateCode(Outputs.IOutput output, API api)
        {
            var suffix = Utils.GetMethodCallerName(this.args);
            if (add_this){
                output.WriteString($"this.");
            }
            this.identifier.GenerateCode(output, api);
            output.WriteString($"{suffix}(");
            var i = 0;
            foreach (var arg in this.args)
            {
                arg.argument.GenerateCode(output, api);
                if(i != this.args.Count -1 && this.args.Count > 1)
                    output.WriteString(", ");
                i++;
            }
            output.WriteString(")");
        }
    }
}