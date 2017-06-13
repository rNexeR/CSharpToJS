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

        public void Evaluate(API api, ContextManager ctx_man){
            foreach(var expr in this.exprs){
                var type = expr.EvaluateType(api, ctx_man);
                if(type.ToString() != "IntType")
                    throw new SemanticException("Expression used as array index must return a int value.");
            }
        }
    }
}