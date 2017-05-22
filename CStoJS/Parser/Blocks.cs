using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void OptionalBodyEnd(){
            printDebug("OptionalBodyEnd");
            if( !OptionalMatchExactly(new TokenType[]{ TokenType.END_STATEMENT }) ){
                //EPSILON
            }

        }

        void  MaybeEmptyBlock(){
            printDebug("Maybe Empty Block");
            if(Match( TokenType.BRACE_OPEN )){
                ConsumeToken();
                OptionalStatementList();
                MatchExactly( new TokenType[]{ TokenType.BRACE_CLOSE } );
            }else{
                MatchExactly( new TokenType[]{ TokenType.END_STATEMENT } );
            }
        }
    }
}