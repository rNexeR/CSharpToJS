using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries{
	public partial class Parser
    {
        void IncrementDecrement(){
            if(Match( TokenType.OP_INC_PP )){
                MatchExactly( new TokenType[] { TokenType.OP_INC_PP });
            }else{
                MatchExactly( new TokenType[] { TokenType.OP_INC_MM });
            }
        }

        void ExpressionUnaryOperator(){
            MatchOne(this.unary_operators, "Unary operator expected");
        }
    }
}