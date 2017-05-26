using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void Type(){
            printDebug("Type");
            MatchOne( this.types, "Type Expected" );
            
            var identifier = new List<Token>();
            IdentifierAttribute(ref identifier);
        }

        void TypeOrVoid(){
            printDebug("Type or Void");
            MatchOne( this.types.Concat( new TokenType[]{ TokenType.VOID_KEYWORD } ).ToArray(), "Type or Void Expected" );
            
            var identifier = new List<Token>();
            IdentifierAttribute(ref identifier);
        }

        void TypeOrVar(){
            printDebug("Type or Void");
            MatchOne( this.types.Concat( new TokenType[]{ TokenType.VAR_KEYWORD } ).ToArray(), "Type or Void Expected" );
            
            var identifier = new List<Token>();
            IdentifierAttribute(ref identifier);
        }
        
    }
}