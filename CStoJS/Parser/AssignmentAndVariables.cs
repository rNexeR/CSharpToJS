using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries{
	public partial class Parser
    {
        void AssignmentOptions(ref List<EnumNode> identifier, ref EnumNode actual){
            printDebug("Assignment Options");
            if( !Match( TokenType.OP_ASSIGN ) ){
                OptionalAssignableIdentifiersListPrime(ref identifier);
            }else{
                MatchExactly(new TokenType[]{ TokenType.OP_ASSIGN });
                actual.assignment = new ExpressionNode();
                Expression();
                OptionalAssignableIdentifiersListPrime(ref identifier);
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