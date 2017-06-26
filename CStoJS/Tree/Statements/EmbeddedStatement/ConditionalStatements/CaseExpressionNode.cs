using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class CaseExpressionNode
    {
        public List<CaseNode> toCompareValue;
        public List<StatementNode> body;

        public CaseExpressionNode()
        {
            this.toCompareValue = new List<CaseNode>();
            this.body = new List<StatementNode>();
        }

        public CaseExpressionNode(List<CaseNode> toCompare, List<StatementNode> body)
        {
            this.toCompareValue = toCompare;
            this.body = body;
        }

        public TypeDeclarationNode EvaluateSemantic(API api, ContextManager context_manager, TypeDeclarationNode eval_type)
        {
            foreach (var to_compare in this.toCompareValue)
            {
                to_compare.EvaluateSemantic(api, context_manager, eval_type);
            }

            TypeDeclarationNode body_ret = null;

            foreach (var st in this.body)
            {
                var st_ret = st.EvaluateSemantic(api, context_manager);
                if (body_ret == null && st_ret != null)
                {
                    body_ret = st_ret;
                }
                if (body_ret != null && st_ret != null)
                {
                    if (body_ret.ToString() != st_ret.ToString())
                        throw new SemanticException($"Multiple return type detected. {body_ret} and {st_ret}.");
                }
            }
            return body_ret;
        }

        public void GenerateCode(Outputs.IOutput output, API api)
        {
            for (int i = 0; i < this.toCompareValue.Count; i++)
            {
                output.WriteString($"\t\t\t{this.toCompareValue[i].label.lexema} ");
                if (toCompareValue[i].value != null)
                    this.toCompareValue[i].value.GenerateCode(output, api);
                output.WriteString(": ");
            }

            foreach(var _body in this.body){
                _body.GenerateCode(output, api);
            }
        }
    }
}