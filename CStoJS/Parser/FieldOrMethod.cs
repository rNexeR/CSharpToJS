using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void FieldOrMethodFactorized(){
            printDebug("Field Or Method Factorized");
            IdentifierAttribute();
            if (Match(TokenType.ID)){
                ConsumeToken();
                FieldOrMethod();
            }
        }

        private void FieldOrMethod()
        {
            printDebug("Field Or Method");
            if( Match(TokenType.PAREN_OPEN) ){
                MethodDeclaration();
            }else{
                FieldDeclaration();
            }
        }

        void ConstructorDeclaration(){
            printDebug("Constructor Declaration");
            MatchExactly( new TokenType[]{ TokenType.PAREN_OPEN } );
            FixedParameters();
            MatchExactly( new TokenType[]{ TokenType.PAREN_CLOSE } );
            ConstructorInitializer();
            MaybeEmptyBlock();
        }

        private void ConstructorInitializer()
        {
            printDebug("Constructor Initializer");
            if(Match(TokenType.OP_MEMBER_ACCESS)){
                MatchExactly( new TokenType[]{ TokenType.OP_MEMBER_ACCESS, TokenType.BASE_KEYWORD, TokenType.PAREN_OPEN } );
                ArgumentList();
                MatchExactly( new TokenType[]{ TokenType.PAREN_CLOSE } );
            }else{
                //EPSILON
            }
        }

        void MethodDeclaration(){
            printDebug("Method Declaration");
            MatchExactly( new TokenType[]{ TokenType.PAREN_OPEN } );
            FixedParameters();
            MatchExactly( new TokenType[]{ TokenType.PAREN_CLOSE } );
            MaybeEmptyBlock();
        }

        void FieldDeclaration(){
            printDebug("Field Declaration");
            VariableAssigner();
            VariableDeclaratorListPrime();
            MatchExactly( new TokenType[]{ TokenType.END_STATEMENT } );
        }
    }
}