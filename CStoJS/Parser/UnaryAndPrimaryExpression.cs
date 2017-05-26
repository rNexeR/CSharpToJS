using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Collections.Generic;

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
            if (ConsumeOnMatch(TokenType.NEW_KEYWORD))
            {
                InstanceExpression();
                PrimaryExpressionPrime();
            }
            else if (MatchAny(this.literals))
            {
                ConsumeToken();
                PrimaryExpressionPrime();
            }
            else if (ConsumeOnMatch(TokenType.ID))
            {
                PrimaryExpressionPrime();
            }
            else if (ConsumeOnMatch(TokenType.PAREN_OPEN))
            {
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
            else if (Match(TokenType.PAREN_OPEN))
            {
                OptionalFunctCall();
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
            if (ConsumeOnMatch(TokenType.PAREN_OPEN))
            {
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
            Type();
            InstanceExpressionFactorized();
        }

        private void InstanceExpressionFactorized()
        {
            MatchExactly(TokenType.PAREN_OPEN);
            ArgumentList();
            MatchExactly(TokenType.PAREN_CLOSE);
        }

        private void ExpressionList()
        {
            Expression();
            // MatchExactly(TokenType.COMMA);
            ExpressionListPrime();
        }

        private void ExpressionListPrime()
        {
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