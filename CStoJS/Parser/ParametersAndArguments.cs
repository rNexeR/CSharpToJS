using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private void FixedParameters()
        {
            printDebug("Fixed Parameters");
            if( MatchAny(this.types) ){
                FixedParameter();
                FixedParametersPrime();
            }else{
                //EPSILON    
            }
        }

        private void FixedParameter()
        {
            printDebug("Fixed Parameter");
             Type();
             MatchExactly( new TokenType[]{ TokenType.ID } );
        }

        private void FixedParametersPrime()
        {
            printDebug("Fixed ParametersPrime");
            if( Match(TokenType.COMMA) ){
                MatchExactly( new TokenType[]{ TokenType.COMMA } );
                FixedParameter();
                FixedParametersPrime();
            }else{
                //EPSILON
            }
            
        }

        void ArgumentList(){
            printDebug("Argument List");
            if( true /* Is it a expression? */ ){
                Expression();
                ArgumentListPrime();
            }else{
                //EPSILON
            }
        }

        void ArgumentListPrime(){
            printDebug("Argument List Prime");
        }

    }
}