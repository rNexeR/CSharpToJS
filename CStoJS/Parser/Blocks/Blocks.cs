using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void OptionalBodyEnd(){
            printDebug("OptionalBodyEnd");
            if( Match( TokenType.END_STATEMENT) ){
                ConsumeToken();
            }else{
                //epsilon
            }

        }

        StatementNode MaybeEmptyBlock(){
            printDebug("Maybe Empty Block");
            if(Match( TokenType.BRACE_OPEN )){
                ConsumeToken();
                var smts = OptionalStatementList();
                MatchExactly( new TokenType[]{ TokenType.BRACE_CLOSE } );
                return new BlockStatementNode(smts);
            }else{
                MatchExactly( new TokenType[]{ TokenType.END_STATEMENT } );
                return null;
            }
        }
    }
}