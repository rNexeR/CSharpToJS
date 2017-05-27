using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private void UnaryExpression()
        {
            printDebug("Unary Expression");
            if (MatchAny(this.unary_operators))
            {
                ConsumeToken();
                UnaryExpression();
            }
            else
            {
                // this.lookAhead = new Token[]{};
                if (ConsumeOnMatchLA(TokenType.PAREN_OPEN) && MatchAndComsumeAnyLA(this.types) && MatchAndComsumeAnyLA(new TokenType[] { TokenType.PAREN_CLOSE, TokenType.OP_MEMBER_ACCESS }))
                {
                    printDebug("\t==> Casting");
                    if (lookAhead[2].type == TokenType.OP_MEMBER_ACCESS)
                    {
                        ConsumeToken();

                        var identifier = new List<Token>();
                        IdentifierAttribute(ref identifier);

                        MatchExactly(TokenType.PAREN_CLOSE);
                    }
                    this.lookAhead = new Token[] { };
                    PrimaryExpression();
                }
                else
                {
                    if (lookAhead.Length > 0 && lookAhead[0].type == TokenType.PAREN_OPEN && lookAheadBack == false)
                    {
                        // Console.WriteLine("Rollback unary");
                        RollbackLA();
                    }
                    PrimaryExpression();
                }
            }
        }

        private void PrimaryExpression()
        {
            printDebug($"Primary Expression {currentToken.lexema}");
            if (Match(TokenType.NEW_KEYWORD))
            {
                ConsumeToken();
                InstanceExpression();
                PrimaryExpressionPrime();
            }
            else if (MatchAny(this.literals))
            {
                ConsumeToken();
                PrimaryExpressionPrime();
            }
            else if (Match(TokenType.ID))
            {
                ConsumeToken();
                PrimaryExpressionPrime();
            }
            else if (Match(TokenType.PAREN_OPEN))
            {
                ConsumeToken();
                printDebug("\t==> '(' Detected");
                Expression();
                printDebug("\t==> After Expression must be )");
                MatchExactly(TokenType.PAREN_CLOSE);
                PrimaryExpressionPrime();
            }
            else if (Match(TokenType.THIS_KEYWORD))
            {
                MatchExactly(TokenType.THIS_KEYWORD);
                PrimaryExpressionPrime();
            }
            else if (Match(TokenType.BASE_KEYWORD))
            {
                MatchExactly(TokenType.BASE_KEYWORD);
                PrimaryExpressionPrime();
            }
            else
            {
                ThrowSyntaxException("new, literal, identifier, '(' or 'this' expected");
            }
        }

        private void PrimaryExpressionPrime()
        {
            printDebug("Primary Expression Prime");
            if (Match(TokenType.OP_MEMBER_ACCESS))
            {
                MatchExactly(new TokenType[] { TokenType.OP_MEMBER_ACCESS, TokenType.ID });
                PrimaryExpressionPrime();
            }
            else if (Match(TokenType.PAREN_OPEN) || Match(TokenType.BRACKET_OPEN) )
            {
                OPtionalFuncOrArrayCall();
                PrimaryExpressionPrime();
            }
            else if (MatchAny(this.increment_decrement_operators))
            {
                ConsumeToken();
                PrimaryExpressionPrime();
            }
            else
            {
                //epsilon
            }
        }

        private void OptionalFunctCall()
        {
            printDebug("Optional Funct Call");
            if (Match(TokenType.PAREN_OPEN))
            {
                MatchExactly(TokenType.PAREN_OPEN);
                ArgumentList();
                MatchExactly(TokenType.PAREN_CLOSE);
            }
            else
            {
                // epsilon
            }
        }

        private void InstanceExpression()
        {
            printDebug("Instance Expression");
            // Type();
            if(Match(TokenType.ID)){
                ConsumeToken();
                var x = new List<Token>();
                IdentifierAttribute(ref x);
            }else{
                ConsumeToken();
            }
            InstanceExpressionFactorized();
        }

        private void InstanceExpressionFactorized()
        {
            printDebug("Instance Expression Factorized");
            // MatchExactly(TokenType.PAREN_OPEN);
            // ArgumentList();
            // MatchExactly(TokenType.PAREN_CLOSE);

            if (Match(TokenType.BRACKET_OPEN))
            {
                MatchExactly(TokenType.BRACKET_OPEN);
                InstanceExpressionFactorizedPrime();
            }
            else if (Match(TokenType.PAREN_OPEN))
            {
                MatchExactly(TokenType.PAREN_OPEN);
                ArgumentList();
                MatchExactly(TokenType.PAREN_CLOSE);
            }
            else
            {
                ThrowSyntaxException("Open bracket or Open brace expected");
            }


        }

        private void InstanceExpressionFactorizedPrime()
        {
            printDebug("Instance Expression Factorized Prime");
            var nuevo = new TokenType[]{ TokenType.OP_TERNARY, TokenType.OP_HIERARCHY,
                TokenType.OP_NULL_COALESCING, TokenType.OP_CONDITIONAL_OR,
                TokenType.OP_CONDITIONAL_OR, TokenType.OP_BITS_OR,
                TokenType.OP_BITS_XOR, TokenType.OP_BITS_AND,
                TokenType.PAREN_OPEN, TokenType.NEW_KEYWORD,
                TokenType.ID, TokenType.THIS_KEYWORD
            };

            
            if (MatchAny(this.expression_operators))
            {
                ExpressionList();
                MatchExactly(TokenType.BRACKET_CLOSE);
                var arr = new ArrayType();

                OptionalRankSpecifierList(ref arr);
                OptionalArrayInitializer();

            }else if(Match(TokenType.BRACKET_CLOSE) || Match(TokenType.COMMA)){

                var arr = new ArrayType();
                RankSpecifierList(ref arr);
                
                ArrayInitializer();
            }else{
                ThrowSyntaxException("Expression or rank specifier expected");
            }
        }

        private void ExpressionList()
        {
            printDebug("Expression List");
            Expression();
            // MatchExactly(TokenType.COMMA);
            ExpressionListPrime();
        }

        private void ExpressionListPrime()
        {
            printDebug("Expression List Prime");
            if (ConsumeOnMatch(TokenType.COMMA))
            {
                ExpressionList();
            }
            else
            {
                //EPSILON
            }
        }

        private void OptionalExpressionList()
        {
            printDebug("Optional Expression List");
            if (MatchAny(this.expression_operators))
            {
                ExpressionList();
            }
            else
            {
                //epsilon
            }
        }
    }
}