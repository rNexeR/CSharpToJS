using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;
using System.Linq;
using System;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        List<StatementNode> OptionalStatementList()
        {
            printDebug("Optional Statement List");
            TokenType[] nuevo = new TokenType[]{
                TokenType.VAR_KEYWORD, TokenType.BRACE_OPEN, TokenType.END_STATEMENT
            }.Concat(iteration_statements).Concat(selection_statements).Concat(jump_statements).ToArray();

            if (MatchAny(nuevo.Concat(unary_operators).Concat(types).Concat(literals).Concat(unary_expression_options).ToArray()))
            {
                return StatementList();
            }
            else
            {
                return new List<StatementNode>();
            }
        }

        private List<StatementNode> StatementList()
        {
            printDebug("Statement List");
            var stmt = Statement();
            var stmts = OptionalStatementList();
            stmts.Insert(0, stmt);

            return stmts;
        }

        StatementNode LocalVariableDeclarationCaller()
        {
            RollbackLA();
            var stmt = LocalVariableDeclaration();
            MatchExactly(TokenType.END_STATEMENT);
            return stmt;
        }

        StatementNode EmbeddedStatementCaller()
        {
            printDebug("LA=>");
            if (lookAhead.Length > 0 && this.types.Concat(new TokenType[] { TokenType.VAR_KEYWORD }).ToArray().Contains(lookAhead[0].type))
            {
                RollbackLA();
            }
            else
            {
                lookAhead = new Token[] { };
            }
            printDebug("==> Llego aqui");
            if (MatchAny(new TokenType[] { TokenType.BRACE_OPEN, TokenType.END_STATEMENT }.Concat(selection_statements).Concat(iteration_statements).Concat(jump_statements).Concat(unary_operators).Concat(literals).Concat(unary_expression_options).ToArray()))
            {
                return EmbeddedStatement();
            }
            else
            {
                ThrowSyntaxException("Local Variable Declaration or Embedded Statement expected");
                return null;
            }
        }

        private StatementNode Statement()
        {
            printDebug("Statement");
            TokenType[] embedded = new TokenType[]{
                TokenType.BRACE_OPEN,TokenType.END_STATEMENT
            }.Concat(iteration_statements).Concat(selection_statements).Concat(jump_statements).ToArray();

            int index = lookAhead.Length;

            if (MatchAndComsumeAnyLA(this.types.Concat(new TokenType[] { TokenType.VAR_KEYWORD }).ToArray()))
            {
                if (lookAhead[index].type != TokenType.ID)
                {
                    return LocalVariableDeclarationCaller();
                }
                else
                {
                    IdentifierAttributeLA();

                    if (ConsumeOnMatchLA(TokenType.ID))
                    {
                        return LocalVariableDeclarationCaller();
                    }
                    else if (ConsumeOnMatchLA(TokenType.BRACKET_OPEN) && (ConsumeOnMatchLA(TokenType.BRACKET_CLOSE) || ConsumeOnMatchLA(TokenType.COMMA)))
                    {
                        return LocalVariableDeclarationCaller();
                    }
                    else
                    {
                        return EmbeddedStatementCaller();
                    }
                }
            }
            else
            {
                return EmbeddedStatementCaller();
            }
        }

        private StatementNode EmbeddedStatement()
        {
            printDebug("Embedded Statement");
            if (!MatchAny(new TokenType[] { TokenType.BRACE_OPEN, TokenType.END_STATEMENT }.Concat(selection_statements).Concat(iteration_statements).Concat(jump_statements).Concat(unary_operators).Concat(literals).Concat(unary_expression_options).ToArray()))
                ThrowSyntaxException("Maybe Empty Block, Statement Expression, Selection Statement, Iteration Statement or Jump Statement expected");

            if (MatchAny(new TokenType[] { TokenType.BRACE_OPEN, TokenType.END_STATEMENT }))
            {
                return MaybeEmptyBlock();
            }
            else if (MatchAny(unary_operators.Concat(literals).Concat(unary_expression_options).ToArray()))
            {
                var stmt = StatementExpression();
                MatchExactly(TokenType.END_STATEMENT);
                return stmt;
            }
            else if (MatchAny(selection_statements))
            {
                return SelectionStateMent();
            }
            else if (MatchAny(iteration_statements))
            {
                return IterationStatement();
            }
            else
            {
                var jump = JumpStatement();
                MatchExactly(TokenType.END_STATEMENT);
                return jump;
            }
        }

        private StatementNode StatementExpression()
        {
            printDebug("Statement Expression");
            var left = UnaryExpression();
            return new StatementExpressionNode(StatementExpressionFactorized(ref left));
            // return StatementExpressionFactorized(ref left);
        }

        private ExpressionNode StatementExpressionFactorized(ref ExpressionNode left)
        {
            if (MatchAny(assignment_operators))
            {
                var operador = ConsumeToken();
                var right = Expression();
                return new AssignationExpressionNode(left, operador, right) as ExpressionNode;
                
            }
            else
            {
                return left;
            }
        }

        // private StatementNode StatementExpressionPrime(ref ExpressionNode left)
        // {
        //     if (ConsumeOnMatch(TokenType.PAREN_OPEN))
        //     {
        //         var args = ArgumentList();
        //         MatchExactly(TokenType.PAREN_CLOSE);
        //     }
        //     else if (MatchAny(increment_decrement_operators))
        //     {
        //         IncrementDecrement();
        //     }
        //     else
        //     {
        //         return null;
        //     }
        // }

        private StatementNode LocalVariableDeclaration()
        {
            printDebug("Local Variable Declaration");
            if (!MatchAny(this.types.Concat(new TokenType[] { TokenType.VAR_KEYWORD }).ToArray()))
            {
                ThrowSyntaxException("Type or Var expected");
            }

            TypeDeclarationNode type = null;

            if (Match(TokenType.VAR_KEYWORD))
            {
                var token = ConsumeToken();
                type = TypeDetector(token.type, new IdentifierNode(token));
            }else{
                type = Type();
            }
            var variables = VariableDeclaratorList(null, null, type);
            var variablesNodes = new List<LocalVariableNode>();

            foreach(var x in variables){
                var y = new LocalVariableNode(x.type, x.identifier, x.assignment);
                variablesNodes.Add(y);
            }

            return new LocalVariablesNode(variablesNodes);
        }

        private List<StatementNode> OptionalStatementExpressionList()
        {
            printDebug("Optional Statement Expression List");
            if (MatchAny(unary_expression_options.Concat(unary_operators).Concat(literals).ToArray()))
            {
                return StatementExpressionList();
            }
            else
            {
                return null;
            }
        }

        private List<StatementNode> StatementExpressionList()
        {
            printDebug("Statement Expression List");

            var stmt = StatementExpression();
            var stmts = StatementExpressionListPrime();

            stmts.Insert(0, stmt);
            return stmts;
        }

        private List<StatementNode> StatementExpressionListPrime()
        {
            if (Match(TokenType.COMMA))
            {
                ConsumeToken();
                var stmt = StatementExpression();
                var stmts = StatementExpressionListPrime();

                stmts.Insert(0, stmt);
                return stmts;
            }
            else
            {
                return new List<StatementNode>();
            }
        }
    }
}