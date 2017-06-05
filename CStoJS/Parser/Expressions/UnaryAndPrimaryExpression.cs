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
        private ExpressionNode UnaryExpression()
        {
            printDebug("Unary Expression");
            if (MatchAny(this.unary_operators))
            {
                var operador = ConsumeToken();
                var expr = UnaryExpression();
                return new PreOperatorExpressionNode(operador, expr);
            }
            else
            {
                // this.lookAhead = new Token[]{};
                if (ConsumeOnMatchLA(TokenType.PAREN_OPEN) && MatchAndComsumeAnyLA(this.types) && MatchAndComsumeAnyLA(new TokenType[] { TokenType.PAREN_CLOSE, TokenType.OP_MEMBER_ACCESS }))
                {
                    printDebug("\t==> Casting");
                    var f_type = lookAhead[1];
                    var identifier = new List<Token>();
                    identifier.Add(f_type);
                    if (lookAhead[2].type == TokenType.OP_MEMBER_ACCESS)
                    {
                        identifier.Add(ConsumeToken());

                        IdentifierAttribute(ref identifier);

                        MatchExactly(TokenType.PAREN_CLOSE);
                    }
                    this.lookAhead = new Token[] { };
                    var expr = PrimaryExpression();
                    var ret = new List<ExpressionNode>();
                    ret.Add(new CastingExpressionNode(new IdentifierTypeNode(new IdentifierNode(identifier)), expr));
                    return new InlineExpressionNode(ret);
                }
                else
                {
                    if (lookAhead.Length > 0 && lookAhead[0].type == TokenType.PAREN_OPEN && lookAheadBack == false)
                    {
                        // Console.WriteLine("Rollback unary");
                        RollbackLA();
                    }
                    return new InlineExpressionNode(PrimaryExpression());
                }
            }
        }

        private List<ExpressionNode> PrimaryExpression()
        {
            printDebug("Primary Expression");
            if (Match(TokenType.NEW_KEYWORD))
            {
                ConsumeToken();
                var left = InstanceExpression();
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();

                ret.Add(left);
                ret.AddRange(right);

                return ret;
            }
            else if (MatchAny(this.literals))
            {
                var token = ConsumeToken();
                var left = new LiteralExpressionNode(token) as ExpressionNode;
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();

                ret.Add(left);
                ret.AddRange(right);
                return ret;

            }
            else if (Match(TokenType.ID))
            {
                var token = ConsumeToken();
                var left = new IdentifierExpressionNode(token) as ExpressionNode;
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();
                if (right.Count > 0 && ( (right[0] is FunctionCallExpressionNode && ((FunctionCallExpressionNode)right[0]).identifier == null) || (right[0] is ArrayAccessExpressionNode && ((ArrayAccessExpressionNode)right[0]).identifier == null)))
                {
                    if(right[0] is FunctionCallExpressionNode ){
                        ((FunctionCallExpressionNode)right[0]).identifier = token;
                    }else if(right[0] is ArrayAccessExpressionNode){
                        ((ArrayAccessExpressionNode)right[0]).identifier = token;
                    }
                }
                else
                {
                    ret.Add(left);
                }
                ret.AddRange(right);

                return ret;
            }
            else if (Match(TokenType.PAREN_OPEN))
            {
                ConsumeToken();
                var expr = Expression();
                MatchExactly(TokenType.PAREN_CLOSE);

                var right = new ParenthesizedExpressionNode(PrimaryExpressionPrime());

                var ret = new List<ExpressionNode>();
                ret.Add(expr);
                ret.Add(right);

                return ret;
            }
            else if (Match(TokenType.THIS_KEYWORD))
            {
                var token = MatchExactly(TokenType.THIS_KEYWORD);
                var left = new ReferenceAccessNode(token) as ExpressionNode;
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();
                ret.Add(left);
                ret.AddRange(right);

                return ret;
            }
            else if (Match(TokenType.BASE_KEYWORD))
            {
                var token = MatchExactly(TokenType.BASE_KEYWORD);
                var left = new ReferenceAccessNode(token) as ExpressionNode;
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();
                ret.Add(left);
                ret.AddRange(right);

                return ret;
            }
            else if (MatchAny(this.buildInTypes))
            {
                var token = ConsumeToken();
                var left = new BuiltInTypeExpressionNode(token) as ExpressionNode;
                var right = PrimaryExpressionPrime();
                var ret = new List<ExpressionNode>();
                ret.Add(left);
                ret.AddRange(right);

                return ret;
            }
            else
            {
                ThrowSyntaxException("new, literal, identifier, '(' or 'this' expected");
                return null;
            }
        }

        private List<ExpressionNode> PrimaryExpressionPrime()
        {
            printDebug("Primary Expression Prime");
            if (Match(TokenType.OP_MEMBER_ACCESS))
            {
                var tokens = MatchExactly(new TokenType[] { TokenType.OP_MEMBER_ACCESS, TokenType.ID });
                var left = new AccessMemoryExpressionNode(tokens[1]) as ExpressionNode;
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();
                
                if (right.Count > 0 && (right[0] is FunctionCallExpressionNode || right[0] is ArrayAccessExpressionNode))
                {
                    if(right[0] is FunctionCallExpressionNode ){
                        ((FunctionCallExpressionNode)right[0]).identifier = tokens[1];
                    }else if(right[0] is ArrayAccessExpressionNode){
                        ((ArrayAccessExpressionNode)right[0]).identifier = tokens[1];
                    }
                }
                else
                {
                    ret.Add(left);
                }

                ret.AddRange(right);

                return ret;
            }
            else if (Match(TokenType.PAREN_OPEN) || Match(TokenType.BRACKET_OPEN))
            {
                var left = FuncOrArrayCall();
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();
                ret.Add(left);
                ret.AddRange(right);

                return ret;
            }
            else if (MatchAny(this.increment_decrement_operators))
            {
                var token = ConsumeToken();
                var left = new PostAdditiveExpressionNode(token) as ExpressionNode;
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();
                ret.Add(left);
                ret.AddRange(right);

                return ret;
            }
            else
            {
                return new List<ExpressionNode>();
            }
        }

        // private void OptionalFunctCall()
        // {
        //     printDebug("Optional Funct Call");
        //     if (Match(TokenType.PAREN_OPEN))
        //     {
        //         MatchExactly(TokenType.PAREN_OPEN);
        //         ArgumentList();
        //         MatchExactly(TokenType.PAREN_CLOSE);
        //     }
        //     else
        //     {
        //         // epsilon
        //     }
        // }

        private ExpressionNode InstanceExpression()
        {
            printDebug("Instance Expression");
            var new_left = new IdentifierExpressionNode();
            if (Match(TokenType.ID))
            {
                var id = ConsumeToken();
                var x = new List<Token>();
                x.Add(id);
                IdentifierAttribute(ref x);
                var type = new IdentifierTypeNode(new IdentifierNode(x)) as TypeDeclarationNode;
                return InstanceExpressionFactorized(ref type);
                // return new InstanceExpressionNode(type, initializer);
            }
            else
            {
                var type = TypeDetector(ConsumeToken().type, new IdentifierNode());
                return InstanceExpressionFactorized(ref type);
                // return new InstanceExpressionNode(type, initializer);
            }

        }

        private InstanceInitilizerExpressionNode InstanceExpressionFactorized(ref TypeDeclarationNode type)
        {
            printDebug("Instance Expression Factorized");
            // MatchExactly(TokenType.PAREN_OPEN);
            // ArgumentList();
            // MatchExactly(TokenType.PAREN_CLOSE);

            if (Match(TokenType.BRACKET_OPEN))
            {
                MatchExactly(TokenType.BRACKET_OPEN);
                return InstanceExpressionFactorizedPrime(ref type);
            }
            else if (Match(TokenType.PAREN_OPEN))
            {
                MatchExactly(TokenType.PAREN_OPEN);
                var args = ArgumentList();
                MatchExactly(TokenType.PAREN_CLOSE);
                return new ConstructorCallExpressionNode(type, args);
            }
            else
            {
                ThrowSyntaxException("Open bracket or Open brace expected");
                return null;
            }


        }

        private InstanceInitilizerExpressionNode InstanceExpressionFactorizedPrime(ref TypeDeclarationNode type)
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
                var exprs = ExpressionList();
                MatchExactly(TokenType.BRACKET_CLOSE);
                var arr = new ArrayType();
                arr.baseType = type;

                OptionalRankSpecifierList(ref arr);
                var initializer = OptionalArrayInitializer();

                return new ArrayInitializerExpressionNode(arr, initializer);

            }
            else if (Match(TokenType.BRACKET_CLOSE) || Match(TokenType.COMMA))
            {

                var arr = new ArrayType();
                arr.baseType = type;
                RankSpecifierList(ref arr);

                var initializer = ArrayInitializer();
                return new ArrayInitializerExpressionNode(arr, initializer);
            }
            else
            {
                ThrowSyntaxException("Expression or rank specifier expected");
                return null;
            }
        }

        private List<ExpressionNode> ExpressionList()
        {
            printDebug("Expression List");
            var expr = Expression();
            // MatchExactly(TokenType.COMMA);
            var exprs = ExpressionListPrime();
            exprs.Insert(0, expr);
            return exprs;
        }

        private List<ExpressionNode> ExpressionListPrime()
        {
            printDebug("Expression List Prime");
            if (ConsumeOnMatch(TokenType.COMMA))
            {
                return ExpressionList();
            }
            else
            {
                return new List<ExpressionNode>();
            }
        }

        private List<ExpressionNode> OptionalExpressionList()
        {
            printDebug("Optional Expression List");
            if (MatchAny(this.expression_operators))
            {
                return ExpressionList();
            }
            else
            {
                return null;
            }
        }
    }
}