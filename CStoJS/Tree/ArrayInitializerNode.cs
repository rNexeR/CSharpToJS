using System;
using System.Collections.Generic;
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

        public ArrayInitializerNode(){
            
        }

        // public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        // {
        //     throw new NotImplementedException();
        // }
    }
}