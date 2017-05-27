using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void FieldOrMethodFactorized(){
            printDebug("Field Or Method Factorized");
            
            var identifier = new List<Token>();
            IdentifierAttribute(ref identifier);
            var arr = new ArrayType();
            OptionalRankSpecifierList(ref arr);
            
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
            if(Match(TokenType.OP_HIERARCHY)){
                MatchExactly( new TokenType[]{ TokenType.OP_HIERARCHY, TokenType.BASE_KEYWORD, TokenType.PAREN_OPEN } );
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