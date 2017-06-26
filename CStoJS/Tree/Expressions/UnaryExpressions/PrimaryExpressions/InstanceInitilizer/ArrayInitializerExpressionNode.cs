using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ArrayInitializerExpressionNode : InstanceInitilizerExpressionNode
    {
        public ArrayType arr;
        public List<ExpressionNode> indexes;
        public string base_type_name;

        public ArrayInitializerExpressionNode(ArrayType arr, List<ExpressionNode> indexes)
        {
            this.arr = arr;
            this.indexes = indexes;
        }

        public ArrayInitializerExpressionNode()
        {

        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            if (this.arr.dimensions == 0)
            {
                if (this.indexes.Count > 1)
                    throw new SemanticException("Invalid rank specifier", this.arr.identifier.identifiers[0]);
            }
            else
            {
                if (this.indexes.Count != this.arr.dimensions + 1)
                    throw new SemanticException("Invalid rank specifier", this.arr.identifier.identifiers[0]);
            }
            if (this.arr.baseType.type != "UserDefined")
                this.base_type_name = this.arr.baseType.type;
            else
            {
                var _usings = class_ctx_man.GetCurrentNamespaceUsings();
                this.base_type_name = Utils.GetClassName(this.arr.baseType.ToString(), _usings, api);
                if (!api.TypeDeclarationExists(base_type_name)                )
                    throw new SemanticException($"Array base type {this.arr.baseType} not found.", this.arr.identifier.identifiers[0]);
                this.base_type_name = "GeneratedCode." + this.base_type_name;
            }
            return arr;
        }

        public override void GenerateCode(Outputs.IOutput output, API api)
        {
            // output.WriteString($" new {this.base_type_name}");
            output.WriteString("[");
            // if (this.arr.dimensions == 0)
            // {
            //     for (int i = 0; i < this.arr.arrayOfArrays; i++)
            //     {
            //         if (this.arr.arrayOfArrays == 1)
            //             break;
            //         output.WriteString("[]");
            //         if (i != this.arr.arrayOfArrays - 1 && this.arr.arrayOfArrays > 1)
            //             output.WriteString(",");
            //     }
            // }
            // else
            // {

            // }
            
            // if (this.arr.dimensions == 0)
            // {
            //     this.indexes[0].GenerateCode(output, api);
            // }
            // else
            // {
            //     var i = 0;
            //     foreach (var expr in this.indexes)
            //     {
            //         expr.GenerateCode(output, api);
            //         if (i != this.arr.dimensions && this.arr.dimensions > 0)
            //         {
            //             output.WriteString(",");
            //         }
            //         i++;
            //     }
            // }
            output.WriteString("]");
        }
    }
}