using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        TypeDeclarationNode Type()
        {
            printDebug("Type");
            var token = MatchOne(this.types, "Type Expected");

            var identifier = new List<Token>();
            identifier.Add(token);
            IdentifierAttribute(ref identifier);

            var type = TypeDetector(token.type, new IdentifierNode(identifier));
            var optionalArrayType = new ArrayType();

            OptionalRankSpecifierList(ref optionalArrayType);

            if(optionalArrayType.arrayOfArrays > 0 || optionalArrayType.dimensions > 0){
                optionalArrayType.baseType = type;
                return optionalArrayType;
            }

            return type;
        }

        TypeDeclarationNode TypeOrVoid()
        {
            printDebug("Type or Void");
            if (!Match(TokenType.VOID_KEYWORD))
                return Type();
            else{
                var token = MatchExactly(TokenType.VOID_KEYWORD);
                return new VoidType(new IdentifierNode(token));
            }
        }

        TypeDeclarationNode TypeOrVar()
        {
            printDebug("Type or Void");
            if (!Match(TokenType.VAR_KEYWORD))
                return Type();
            else{
                var token = MatchExactly(TokenType.VAR_KEYWORD);
                return new VarType(new IdentifierNode(token));
            }
        }

    }
}