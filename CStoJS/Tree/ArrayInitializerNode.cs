using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ArrayInitializerNode : VariableInitializer
    {
        public List<VariableInitializer> initializers;

        public ArrayInitializerNode(List<VariableInitializer> initializers)
        {
            this.initializers = initializers;
        }

        public ArrayInitializerNode()
        {

        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            TypeDeclarationNode ret_type = null;
            foreach (var initializer in initializers)
            {
                if (ret_type == null)
                {
                    ret_type = initializer.EvaluateType(api, ctx_man);
                    continue;
                }
                var c_type = initializer.EvaluateType(api, ctx_man);
                if(c_type.ToString() != ret_type.ToString()){
                    //check if it's a child class
                    throw new SemanticException("Array initializer must contain only items of the same type.");
                }
                    
            }
            return ret_type;
        }
    }
}