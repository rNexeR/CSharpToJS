using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private void UnaryExpression()
        {
            printDebug("Unary Expression");
            if( MatchAndComsumeAny(this.unary_operators) ){
                UnaryExpression();
            }else{
                // this.lookAhead = new Token[]{};
                if( ConsumeOnMatchLA(TokenType.PAREN_OPEN) && MatchAndComsumeAnyLA(this.types) && MatchAndComsumeAnyLA(new TokenType[]{ TokenType.PAREN_CLOSE, TokenType.OP_MEMBER_ACCESS }) ){
                    printDebug("\t==> Casting");
                    if(lookAhead[2].type == TokenType.OP_MEMBER_ACCESS){
                        ConsumeToken();
                        IdentifierAttribute();
                        MatchExactly(TokenType.PAREN_CLOSE);
                    }
                    this.lookAhead = new Token[]{};
                    PrimaryExpression();
                }else{
                    if(lookAhead.Length > 0 && lookAhead[0].type == TokenType.PAREN_OPEN)
                        RollbackLA();
                    PrimaryExpression();
                }
            }
        }

        private void PrimaryExpression()
        {
            printDebug("Primary Expression");
            if( ConsumeOnMatch(TokenType.NEW_KEYWORD) ){
                InstanceExpression();
                PrimaryExpressionPrime();
            }else if( MatchAndComsumeAny(this.literals) ){
                PrimaryExpressionPrime();
            }else if( ConsumeOnMatch(TokenType.ID) ){
                PrimaryExpressionPrime();
            }else if( ConsumeOnMatch(TokenType.PAREN_OPEN)){
                printDebug("\t==> '(' Detected");
                Expression();
                printDebug("\t==> After Expression must be )");
                MatchExactly(TokenType.PAREN_CLOSE);
                PrimaryExpressionPrime();
            }else if(Match(TokenType.THIS_KEYWORD)){
                MatchExactly( TokenType.THIS_KEYWORD );
                PrimaryExpressionPrime();
            }else{
                ThrowSyntaxException("new, literal, identifier, '(' or 'this' expected");
            }
        }

        private void PrimaryExpressionPrime()
        {
            printDebug("Primary Expression Prime");
            if( OptionalMatchExactly( new TokenType[]{ TokenType.OP_MEMBER_ACCESS, TokenType.ID } ) ){
                PrimaryExpressionPrime();
            }else if(Match(TokenType.PAREN_OPEN) || Match(TokenType.BRACKET_OPEN)){
                OptionalFunctOrArrayCall();
            }
            else if( MatchAndComsumeAny(this.increment_decrement_operators) ){
                PrimaryExpressionPrime();
            }else{
               //epsilon
            }
        }

        private void OptionalFunctOrArrayCall()
        {
            printDebug("Optional Funct Or Array Call");
            if( ConsumeOnMatch(TokenType.PAREN_OPEN) ){
                ArgumentList();
                MatchExactly(TokenType.PAREN_CLOSE);
            }else if( Match(TokenType.BRACKET_OPEN) ){
                OptionalArrayAccessList();
            }else{
                // epsilon
            }
        }

        private void InstanceExpression()
        {
            Type();
            InstanceExpressionFactorized();
        }

        private void InstanceExpressionFactorized()
        {
            if( Match(TokenType.BRACKET_OPEN) ){
                // if( Match(TokenType.BRACKET_OPEN) ){
                //     RankSpecifierList();
                //     MatchExactly(TokenType.BRACKET_CLOSE);
                //     ArrayInitializer();
                // }else{
                //     ExpressionList();
                //     MatchExactly(TokenType.BRACKET_CLOSE);
                //     OptionalRankSpecifierList();
                //     OptionalArrayInitializer();
                // }
                if( ConsumeOnMatchLA(TokenType.BRACKET_OPEN) && ConsumeOnMatchLA(TokenType.COMMA) ){
                    RollbackLA();
                    RankSpecifierList();
                    ArrayInitializer();
                }else{
                    RollbackLA();
                    ConsumeOnMatch(TokenType.BRACKET_OPEN);
                    ExpressionList();
                    MatchExactly(TokenType.BRACKET_CLOSE);
                    OptionalRankSpecifierList();
                    OptionalArrayInitializer();
                }
            }else{
                MatchExactly(TokenType.PAREN_OPEN);
                ArgumentList();
                MatchExactly(TokenType.PAREN_CLOSE);
            }
        }

        private void ExpressionList()
        {
            Expression();
            // MatchExactly(TokenType.COMMA);
            ExpressionListPrime();
        }

        private void ExpressionListPrime(){
            if(ConsumeOnMatch(TokenType.COMMA)){
                ExpressionList();
            }else{
                //EPSILON
            }
        }

        private void OptionalExpressionList()
        {
            if(MatchAny(this.expression_operators)){
                ExpressionList();
            }else{
                //epsilon
            }
        }
    }
}