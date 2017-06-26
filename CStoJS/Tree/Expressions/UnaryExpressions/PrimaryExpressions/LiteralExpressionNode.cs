using System;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class LiteralExpressionNode : PrimaryExpressionNode
    {
        public Token literal;

        public LiteralExpressionNode() : base(){
            
        }

        public LiteralExpressionNode(Token literal)
        {
            this.literal = literal;
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager class_ctx_man, ContextManager st_ctx_man = null)
        {
            switch(literal.type){
                case TokenType.LITERAL_INT: this.returnType = api.GetTypeDeclaration("IntType"); break;
                case TokenType.LITERAL_CHAR: this.returnType = api.GetTypeDeclaration("CharType"); break;
                case TokenType.TRUE_KEYWORD: this.returnType = api.GetTypeDeclaration("BoolType"); break;
                case TokenType.FALSE_KEYWORD: this.returnType = api.GetTypeDeclaration("BoolType"); break;
                case TokenType.LITERAL_STRING: this.returnType = api.GetTypeDeclaration("StringType"); break;
                case TokenType.LITERAL_STRING_VERBATIM: this.returnType = api.GetTypeDeclaration("StringType"); break;
                default: this.returnType = api.GetTypeDeclaration("FloatType"); break;
            }

            return returnType;
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            literal.lexema = literal.lexema.Replace("f", "");
            output.WriteString(literal.lexema);
        }
    }
}