using System;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ArrayInitializerExpressionNode : InstanceInitilizerExpressionNode
    {
        public ArrayInitializerNode initializer;
        private readonly ArrayType arr;

        public ArrayInitializerExpressionNode(ArrayType arr, ArrayInitializerNode initializer)
        {
            this.arr = arr;
            this.initializer = initializer;
        }

        public ArrayInitializerExpressionNode()
        {

        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            // var usings = ctx_man.GetCurrentNamespaceUsings();
            // string expected_name;
            // if (arr.baseType is ArrayType)
            //     expected_name = arr.ToString();
            // else
            //     expected_name = Utils.GetClassName(arr.baseType.identifier.ToString(), usings, api);
            if (initializer != null)
            {
                var initializers_type = this.initializer.EvaluateType(api, ctx_man);
                if (arr.ToString() != initializers_type.ToString())
                    throw new SemanticException($"Array Initializer types ({initializers_type}) mismatch in type with constructor type ({arr}).");
            }
            //arr.baseType = api.GetTypeDeclaration(expected_name);
            return arr;
        }
    }
}