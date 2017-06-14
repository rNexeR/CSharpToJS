using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ConstructorInitializerNode
    {
        public IdentifierNode identifier;
        public List<ArgumentNode> arguments;

        public ConstructorInitializerNode(){
            this.arguments = new List<ArgumentNode>();
        }

        public ConstructorInitializerNode(IdentifierNode identifier, List<ArgumentNode> args){
            this.identifier = identifier;
            this.arguments = args;
        }

        internal void Evaluate(API api, ContextManager context_manager)
        {
            var args = new List<string>();
            foreach(var arg in this.arguments){
                var arg_type = arg.EvaluateType(api, context_manager);
                args.Add(arg_type.ToString());
            }

            var ctor_name = $"{identifier}({string.Join(",", args)})";
            if(!context_manager.ConstructorExists(ctor_name))
                throw new SemanticException($"Parent class doesn't contain a constructor with prototype: {ctor_name} or it's inaccessible due to its protection level", this.identifier.identifiers[0]);
        }
    }
}