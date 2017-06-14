using System;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ArgumentNode
    {
        public ExpressionNode argument;

        public ArgumentNode(){

        }

        public ArgumentNode(ExpressionNode argument){
            this.argument = argument;
        }

        internal object EvaluateType(API api, ContextManager context_manager)
        {
            return argument.EvaluateType(api, context_manager);
        }
    }
}