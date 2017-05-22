using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {

        private void OptionalCommaList()
        {
            printDebug("Optional Comma List");
            if( Match(TokenType.COMMA) ){
                CommaList();
            }else{
                //epsiloon
            }
        }

        private void CommaList()
        {
            printDebug("Comma List");
            MatchExactly(TokenType.COMMA);
            OptionalCommaList();
        }
    }
}