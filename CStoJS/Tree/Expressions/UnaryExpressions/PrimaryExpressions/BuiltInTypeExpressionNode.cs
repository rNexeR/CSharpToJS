using System;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class BuiltInTypeExpressionNode : PrimaryExpressionNode
    {
        public Token token;

        public BuiltInTypeExpressionNode() : base(){

        }

        public BuiltInTypeExpressionNode(Token token)
        {
            this.token = token;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            switch(token.type){
                case TokenType.INT_KEYWORD: this.returnType =  api.GetTypeDeclaration("IntType"); break;
                case TokenType.CHAR_KEYWORD: this.returnType =  api.GetTypeDeclaration("CharType"); break;
                case TokenType.BOOL_KEYWORD: this.returnType =  api.GetTypeDeclaration("BoolType"); break;
                case TokenType.STRING_KEYWORD: this.returnType =  api.GetTypeDeclaration("StringType"); break;
                default: this.returnType =  api.GetTypeDeclaration("FloatType"); break;
            }
            return this.returnType;
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            output.WriteString($"GeneratedCode.{this.returnType}");
        }
    }
}