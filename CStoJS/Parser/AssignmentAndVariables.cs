using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries{
	public partial class Parser
    {
        void AssignmentOptions(){
            printDebug("Assignment Options");
            if( !Match( TokenType.OP_ASSIGN ) ){
                OptionalAssignableIdentifiersListPrime();
            }else{
                MatchExactly(new TokenType[]{ TokenType.OP_ASSIGN });
                Expression();
                OptionalAssignableIdentifiersListPrime();
            }
        }

        void VariableAssigner(){
            printDebug("Variable Assigner");
            if( Match(TokenType.OP_ASSIGN) ){
                currentToken = lexer.GetNextToken();
                VariableInitializer();
            }else{
                //EPSILON
            }
        }
        void VariableDeclaratorListPrime(){
            printDebug("Variable Declarator List Prime");
            if(Match(TokenType.COMMA)){
                currentToken = lexer.GetNextToken();
                VariableDeclaratorList();
            }else{
                //EPSILON
            }
        }

        void VariableInitializer(){
            printDebug("Variable Initializer");
            //Change this after
            if( MatchAny(this.literals) ){
                Expression();
            }else{
                ArrayInitializer();
            }
        }

        void VariableDeclaratorList(){
            printDebug("Variable Declarator List");
            MatchExactly( new TokenType[]{ TokenType.ID } );
            VariableAssigner();
            VariableDeclaratorListPrime();
        }
    }
}