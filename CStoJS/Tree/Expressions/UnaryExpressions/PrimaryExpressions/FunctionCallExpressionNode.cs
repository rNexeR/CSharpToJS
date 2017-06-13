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

        public FunctionCallExpressionNode(List<ArgumentNode> args)
        {
            this.args = args;
        }

        public FunctionCallExpressionNode(){
            
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            var id_node = identifier as IdentifierExpressionNode;
            var id_name = id_node.token.lexema;

            var args = new List<string>();
            foreach(var arg in this.args){
                args.Add(arg.argument.EvaluateType(api, ctx_man).ToString());
            }
            var fn_name = $"{id_name}({string.Join(",",args)})";
            if(!ctx_man.MethodExists(fn_name))
                throw new SemanticException($"Method {fn_name} doesn't exists.", id_node.token);
            
            return ctx_man.GetMethodReturnType(fn_name);
        }
    }
}