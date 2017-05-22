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

        private void OptionalVariableInitializerList()
        {
            printDebug("Optional Variable Initializer List ==TODO");
            if( MatchAny(this.expression_operators) ){
                VariableInitializerList();
            }else{
                //epsilon
            }
        }

        private void VariableInitializerList()
        {
            VariableInitializer();
            VariableInitializerPrime();
        }

        private void VariableInitializerPrime()
        {
            if(ConsumeOnMatch(TokenType.COMMA)){
                VariableInitializerList();
            }else{
                //epsilomn
            }
        }

        void VariableAssigner(){
            printDebug("Variable Assigner");
            if( Match(TokenType.OP_ASSIGN) ){
                ConsumeToken();
                VariableInitializer();
            }else{
                //EPSILON
            }
        }
        void VariableDeclaratorListPrime(){
            printDebug("Variable Declarator List Prime");
            if(Match(TokenType.COMMA)){
                ConsumeToken();
                VariableDeclaratorList();
            }else{
                //EPSILON
            }
        }

        void VariableInitializer(){
            printDebug("Variable Initializer");
            //Change this after
            if( MatchAny(this.expression_operators) ){
                Expression();
            }else{
                ThrowSyntaxException("VariableInitializer expected");
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