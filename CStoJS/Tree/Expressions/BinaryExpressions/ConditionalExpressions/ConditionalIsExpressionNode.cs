using System;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ConditionalIsExpressionNode: ExpressionNode
    {
        public ExpressionNode left;
        public Token operador;
        public TypeDeclarationNode type;
        private string target_type_name;
        public ConditionalIsExpressionNode() : base()
        {

        }

        public ConditionalIsExpressionNode(ExpressionNode left, Token operador, TypeDeclarationNode right)
        {
            this.left = left;
            this.operador = operador;
            this.type = right;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            var _usings = ctx_man.GetCurrentNamespaceUsings();
            var left_type = left.EvaluateType(api, ctx_man);

            // var left_type_name = Utils.GetClassName(left_type.ToString(), _usings, api);
            target_type_name = Utils.GetClassName(type.ToString(), _usings, api);

            if(!Utils.AreEquivalentsTypes(left_type, type, _usings, api))
                throw new SemanticException($"Cannot cast {left_type} to {type}", type.identifier.identifiers[0]);

            return new BoolType();
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            left.GenerateCode(output, api);
            output.WriteString($" instanceof GeneratedCode.{this.target_type_name}");
        }
    }
}