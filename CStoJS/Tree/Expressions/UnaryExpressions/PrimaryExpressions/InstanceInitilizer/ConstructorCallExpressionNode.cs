using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ConstructorCallExpressionNode : InstanceInitilizerExpressionNode
    {
        public List<ArgumentNode> args;
        public TypeDeclarationNode type;

        public ConstructorCallExpressionNode(TypeDeclarationNode type, List<ArgumentNode> args)
        {
            this.type = type;
            this.args = args;
        }

        public ConstructorCallExpressionNode(){
            
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            var args_types = new List<string>();
            foreach(var arg in this.args){
                var arg_type = arg.argument.EvaluateType(api, class_ctx_man).ToString();
                args_types.Add(arg_type);
            }

            var _usings = class_ctx_man.GetCurrentNamespaceUsings();
            var target_type_name = Utils.GetClassName(type.ToString(), _usings, api);
            var target_type = api.GetTypeDeclaration(target_type_name);
            var ctor_name = $"{target_type.identifier}({string.Join(",", args_types)})";
            if(!api.ClassConstructorExists(target_type as ClassNode, ctor_name))
                throw new SemanticException($"Constructor {ctor_name} doesn't exist in type {target_type_name}.");
            return target_type;
        }
    }
}