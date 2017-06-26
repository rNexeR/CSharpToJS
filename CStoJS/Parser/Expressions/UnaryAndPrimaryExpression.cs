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
                        identifier.Add(currentToken);
                        ConsumeOnMatchLA(TokenType.ID);

                        IdentifierAttributeLA(ref identifier);

                        if(!Match(TokenType.PAREN_CLOSE)){
                            RollbackLA();
                            return new InlineExpressionNode(PrimaryExpression());
                        }

                        MatchExactly(TokenType.PAREN_CLOSE);
                    }

                    if (MatchAny(new TokenType[] { TokenType.OP_INC_MM, TokenType.OP_INC_PP }))
                    {
                        // this.RollbackLA();
                        var right = PrimaryExpressionPrime();
                        var left = new List<ExpressionNode>();
                        foreach (var id in identifier)
                            left.Add(new IdentifierExpressionNode(id));

                        ((right[0]) as PostAdditiveExpressionNode).expression = new InlineExpressionNode(left);
                        return right[0];
                    }
                    else if (Match(TokenType.OP_MEMBER_ACCESS))
                    {
                        var right = PrimaryExpressionPrime();
                        var left = new List<ExpressionNode>();
                        foreach (var id in identifier)
                            left.Add(new IdentifierExpressionNode(id));

                        right.InsertRange(0, left);
                        return new InlineExpressionNode(right);
                    }
                    this.lookAhead = new Token[] { };
                    var expr = PrimaryExpression();
                    var ret = new List<ExpressionNode>();
                    var builtIn = new List<TokenType>(this.builtInTypes);
                    var target = builtIn.Contains(identifier[0].type) ? TypeDetector(identifier[0].type, new IdentifierNode(identifier)) : new IdentifierTypeNode(new IdentifierNode(identifier));
                    var op_1 = new ParenthisizedCastingExpressionNode(target, new InlineExpressionNode(expr));
                    ret.Add(op_1);
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
                if (right.Count > 0 &&
                    (
                        (right[0] is FunctionCallExpressionNode && ((FunctionCallExpressionNode)right[0]).identifier == null)
                        || (right[0] is ArrayAccessExpressionNode && ((ArrayAccessExpressionNode)right[0]).identifier == null)
                        || (right[0] is PostAdditiveExpressionNode && ((PostAdditiveExpressionNode)right[0]).expression == null)
                    )
                )
                {
                    if (right[0] is FunctionCallExpressionNode)
                    {
                        ((FunctionCallExpressionNode)right[0]).identifier = left;
                    }
                    else if (right[0] is ArrayAccessExpressionNode)
                    {
                        ((ArrayAccessExpressionNode)right[0]).identifier = left;
                    }
                    else
                    {
                        ((PostAdditiveExpressionNode)right[0]).expression = left;
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

                var right = PrimaryExpressionPrime();
                //REVISAR EL PRIMARY EXPRESSION PRIME
                if (right.Count > 0 && (right[0] is FunctionCallExpressionNode || right[0] is ArrayAccessExpressionNode || (right[0] is PostAdditiveExpressionNode)))
                {
                    if (right[0] is FunctionCallExpressionNode)
                    {
                        ((FunctionCallExpressionNode)right[0]).identifier = expr;
                    }
                    else if (right[0] is ArrayAccessExpressionNode)
                    {
                        ((ArrayAccessExpressionNode)right[0]).identifier = expr;
                    }
                    else
                    {
                        ((PostAdditiveExpressionNode)right[0]).expression = expr;
                    }
                    return right;
                }
                else
                {
                    var internal_expression = new ParenthesizedExpressionNode(expr);

                    var ret = new List<ExpressionNode>();
                    ret.Add(internal_expression);
                    ret.AddRange(right);

                    return ret;
                }
            }
            else if (Match(TokenType.THIS_KEYWORD))
            {
                var token = MatchExactly(TokenType.THIS_KEYWORD);
                var left = new IdentifierExpressionNode(token) as ExpressionNode;
                var right = PrimaryExpressionPrime("this");

                var ret = new List<ExpressionNode>();
                ret.Add(left);
                ret.AddRange(right);

                return ret;
            }
            else if (Match(TokenType.BASE_KEYWORD))
            {
                var token = MatchExactly(TokenType.BASE_KEYWORD);
                var left = new IdentifierExpressionNode(token) as ExpressionNode;
                var right = PrimaryExpressionPrime("base");

                var ret = new List<ExpressionNode>();
                ret.Add(left);
                ret.AddRange(right);

                return ret;
            }
            else if (Match(TokenType.NULL_KEYWORD))
            {
                var left = new NullExpressionNode(ConsumeToken());
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();
                ret.Add(left);
                ret.AddRange(right);

                return ret;
            }
            else if (MatchAny(this.builtInTypes))
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

        private List<ExpressionNode> PrimaryExpressionPrime(string reference = null)
        {
            printDebug("Primary Expression Prime");
            if (Match(TokenType.OP_MEMBER_ACCESS))
            {
                var tokens = MatchExactly(new TokenType[] { TokenType.OP_MEMBER_ACCESS, TokenType.ID });
                if(reference != null)
                    tokens[1].lexema = $"{reference}.{tokens[1].lexema}";
                var left = new IdentifierExpressionNode(tokens[1]) as ExpressionNode;
                var right = PrimaryExpressionPrime();

                var ret = new List<ExpressionNode>();

                if (right.Count > 0 && (right[0] is FunctionCallExpressionNode || right[0] is ArrayAccessExpressionNode || (right[0] is PostAdditiveExpressionNode)))
                {
                    if (right[0] is FunctionCallExpressionNode)
                    {
                        ((FunctionCallExpressionNode)right[0]).identifier = new IdentifierExpressionNode(tokens[1]);
                    }
                    else if (right[0] is ArrayAccessExpressionNode)
                    {
                        ((ArrayAccessExpressionNode)right[0]).identifier = new IdentifierExpressionNode(tokens[1]);
                    }
                    else
                    {
                        ((PostAdditiveExpressionNode)right[0]).expression = left;
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
                var token = ConsumeToken();
                var type = TypeDetector(token.type, new IdentifierNode(token));
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
                if(exprs.Count == 1)
                    arr.arrayOfArrays = 1;
                else
                    arr.dimensions = exprs.Count-1;

                OptionalRankSpecifierList(ref arr);
                // var initializer = OptionalArrayInitializer();

                return new ArrayInitializerExpressionNode(arr, exprs);

            }
            else if (Match(TokenType.BRACKET_CLOSE) || Match(TokenType.COMMA))
            {

                var arr = new ArrayType();
                arr.baseType = type;
                RankSpecifierList(ref arr);

                // var initializer = ArrayInitializer();
                return new ArrayInitializerExpressionNode(arr, new List<ExpressionNode>());
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