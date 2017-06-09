using System.Collections.Generic;

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
    }
}