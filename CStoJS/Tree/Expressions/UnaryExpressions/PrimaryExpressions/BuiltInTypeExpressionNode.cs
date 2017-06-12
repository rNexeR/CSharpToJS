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

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            switch(token.type){
                case TokenType.INT_KEYWORD: return api.GetTypeDeclaration("IntType");
                case TokenType.CHAR_KEYWORD: return api.GetTypeDeclaration("CharType");
                case TokenType.BOOL_KEYWORD: return api.GetTypeDeclaration("BoolType");
                case TokenType.STRING_KEYWORD: return api.GetTypeDeclaration("StringType");
                default: return api.GetTypeDeclaration("FloatType");
            }
        }
    }
}