using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ArrayAccessNode
    {
        public List<ExpressionNode> exprs;

        public ArrayAccessNode(List<ExpressionNode> exprs)
        {
            this.exprs = exprs;
        }

        public ArrayAccessNode(){
            
        }

        public void Evaluate(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null){
            foreach(var expr in this.exprs){
                var type = expr.EvaluateType(api, class_ctx_man);
                if(type.ToString() != "IntType")
                    throw new SemanticException("Expression used as array index must return a int value.");
            }
        }
    }
}