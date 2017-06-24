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
                case TokenType.LITERAL_INT: return api.GetTypeDeclaration("IntType");
                case TokenType.LITERAL_CHAR: return api.GetTypeDeclaration("CharType");
                case TokenType.TRUE_KEYWORD: return api.GetTypeDeclaration("BoolType");
                case TokenType.FALSE_KEYWORD: return api.GetTypeDeclaration("BoolType");
                case TokenType.LITERAL_STRING: return api.GetTypeDeclaration("StringType");
                case TokenType.LITERAL_STRING_VERBATIM: return api.GetTypeDeclaration("StringType");
                default: return api.GetTypeDeclaration("FloatType");
            }
        }
    }
}