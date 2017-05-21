using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void Type(){
            printDebug("Type");
            MatchOne( this.types, "Type or Void Expected" );
        }

        void TypeOrVoid(){
            printDebug("Type or Void");
            MatchOne( this.types.Concat( new TokenType[]{ TokenType.VOID_KEYWORD } ).ToArray(), "Type or Void Expected" );
        }

        void TypeOrVar(){
            printDebug("Type or Void");
            MatchOne( this.types.Concat( new TokenType[]{ TokenType.VAR_KEYWORD } ).ToArray(), "Type or Void Expected" );
        }
        
    }
}