using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries{
	public partial class Parser
    {
        void OptionalAssignableIdentifiersList(){
            printDebug("Optional Assignable Identifiers List");
            if( OptionalMatchExactly(new TokenType[]{ TokenType.ID } ) ){
                AssignmentOptions();
            }else{
                //EPSILON
            }
        }

        void OptionalAssignableIdentifiersListPrime(){
            printDebug("Optional Assignable Identifiers List Prime");
            if( OptionalMatchExactly(new TokenType[]{ TokenType.COMMA } ) ){
                OptionalAssignableIdentifiersList();
            }else{
                //EPSILON
            }
        }

        void IdentifierList(){
            printDebug("Identifier List");
            MatchExactly( new TokenType[]{ TokenType.ID } );
            IdentifierListPrime();
        }

        void OptionalIdentifierList(){
            printDebug("Optional Identifier List");
            if( Match(TokenType.ID) ){
                IdentifierList();
            }else{
                //EPSILON
            }
        }

        void IdentifierListPrime(){
            printDebug("Identifier List Prime");
            if( Match(TokenType.COMMA) ){
                MatchExactly( new TokenType[]{ TokenType.COMMA, TokenType.ID } );
                IdentifierListPrime();
            }else{
                //EPSILON
            }
        }
    }
}