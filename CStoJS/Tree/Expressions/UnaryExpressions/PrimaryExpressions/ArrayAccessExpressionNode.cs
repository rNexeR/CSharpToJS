using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ArrayAccessExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode identifier;
        public List<ArrayAccessNode> indexes;

        public ArrayAccessExpressionNode(List<ArrayAccessNode> indexes) : base()
        {
            this.indexes = indexes;
        }

        public ArrayAccessExpressionNode() : base()
        {

        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            var ctx_man = st_ctx_man is null ? class_ctx_man : st_ctx_man;
            var id_ret = identifier.EvaluateType(api, ctx_man);

            if (!(id_ret is ArrayType))
                throw new SemanticException("Expression doesn't return an array.");

            var id_array = id_ret as ArrayType;
            if (id_array.dimensions == 0)
            {
                //array of arrays
                if (id_array.arrayOfArrays > this.indexes.Count)
                    throw new SemanticException("Too many array indexes.");

                foreach (var index in indexes)
                {
                    index.Evaluate(api, class_ctx_man);
                    if (index.exprs.Count > (id_ret as ArrayType).arrayOfArrays)
                        throw new SemanticException("Too many array dimensions indexes.");
                }
                var ret = new ArrayType();
                ret.arrayOfArrays = id_array.dimensions - this.indexes.Count;
                ret.baseType = id_array.baseType;
                if(ret.arrayOfArrays <= 0)
                    return ret.baseType;
                return ret;
            }
            else
            {
                //multidimensions
                if(this.indexes.Count > 1)
                    throw new SemanticException("Too many array indexes.");
                foreach (var index in indexes)
                {
                    index.Evaluate(api, class_ctx_man);
                    if (index.exprs.Count != id_array.dimensions + 1)
                        throw new SemanticException("Differents array dimensions indexes.");
                }
                return id_array.baseType;
            }
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            this.identifier.GenerateCode(output, api);
            foreach(var index in this.indexes){
                output.WriteString("[");
                var i = 0;
                foreach(var exp in index.exprs){
                    exp.GenerateCode(output, api);
                    if(i < index.exprs.Count -1 && index.exprs.Count > 1)
                        output.WriteString(",");
                }
                output.WriteString("]");
            }
        }
    }
}