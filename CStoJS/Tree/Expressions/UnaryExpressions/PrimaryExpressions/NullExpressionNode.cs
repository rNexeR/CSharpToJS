using System;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class NullExpressionNode : UnaryExpressionNode
    {
        public Token identifier;
        public NullExpressionNode() : base() { }
        public NullExpressionNode(Token identifier){
            this.identifier = identifier;
        }
        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            this.returnType = new NullType(this.identifier);
            return returnType;
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            output.WriteString("null");
        }
    }
}