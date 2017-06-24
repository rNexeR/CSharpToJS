using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;
using System.Linq;
using System;
using CStoJS.Tree;
using System.Collections.Generic;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private StatementNode IterationStatement()
        {
            printDebug("Iteration Statement");
            if (!MatchAny(iteration_statements))
                ThrowSyntaxException("Iteration Statement expected");

            if (Match(TokenType.WHILE_KEYWORD))
            {
                return WhileStatement();
            }
            else if (Match(TokenType.DO_KEYWORD))
            {
                return DoStatement();
            }
            else if (Match(TokenType.FOR_KEYWORD))
            {
                return ForStatement();
            }
            else
            {
                return ForEachStatement();
            }
        }

        private StatementNode ForEachStatement()
        {
            printDebug("ForEach Statement");
            MatchExactly(TokenType.FOREACH_KEYWORD);
            MatchExactly(TokenType.PAREN_OPEN);
            var type = TypeOrVar();
            var id = MatchExactly(TokenType.ID);
            MatchExactly(TokenType.IN_KEYWORD);
            var collection = Expression();
            MatchExactly(TokenType.PAREN_CLOSE);
            var body = EmbeddedStatement();

            return new ForeachStatementNode(new LocalVariableNode(type, new IdentifierNode(id)), collection, body);
        }

        private StatementNode ForStatement()
        {
            printDebug("For Statement");
            MatchExactly(TokenType.FOR_KEYWORD);
            MatchExactly(TokenType.PAREN_OPEN);
            var initializer = OptionalForInitializer();
            MatchExactly(TokenType.END_STATEMENT);
            var condition = OptionalExpression();
            MatchExactly(TokenType.END_STATEMENT);
            var increment = OptionalStatementExpressionList();
            MatchExactly(TokenType.PAREN_CLOSE);
            var body = EmbeddedStatement();

            return new ForStatementNode(initializer, condition, increment, body);
        }

        private List<StatementNode> OptionalForInitializer()
        {
            printDebug("Optional For Initializer");
            TokenType[] nuevo = { TokenType.OP_TERNARY, TokenType.OP_HIERARCHY,
                TokenType.OP_NULL_COALESCING, TokenType.OP_CONDITIONAL_OR,
                TokenType.OP_CONDITIONAL_AND, TokenType.OP_BITS_OR,
                TokenType.OP_BITS_XOR, TokenType.OP_BITS_AND, TokenType.ID
            };
            if (MatchAndComsumeAnyLA(types.Concat(new TokenType[] { TokenType.VAR_KEYWORD }).ToArray()) && ConsumeOnMatchLA(TokenType.ID))
            {
                RollbackLA();
                var stmt = LocalVariableDeclaration();
                var ret = new List<StatementNode>();
                ret.Add(stmt);
                return ret;
            }
            else if (MatchAny(nuevo.Concat(expression_operators).ToArray()))
            {
                RollbackLA();
                return StatementExpressionList();
            }
            else
            {
                return new List<StatementNode>();
            }
        }

        private StatementNode DoStatement()
        {
            printDebug("Do Statement");
            MatchExactly(TokenType.DO_KEYWORD);
            var body = EmbeddedStatement();
            MatchExactly(TokenType.WHILE_KEYWORD);
            MatchExactly(TokenType.PAREN_OPEN);
            var condition = Expression();
            MatchExactly(TokenType.PAREN_CLOSE);
            MatchExactly(TokenType.END_STATEMENT);
            return new DoStatementNode(condition, body);
        }

        private StatementNode WhileStatement()
        {
            printDebug("While Statement");
            MatchExactly(TokenType.WHILE_KEYWORD);
            MatchExactly(TokenType.PAREN_OPEN);
            var condition = Expression();
            MatchExactly(TokenType.PAREN_CLOSE);
            var body = EmbeddedStatement();

            return new WhileStatementNode(condition,body);
        }

        private JumpStatementNode JumpStatement()
        {
            printDebug("Jump Statement");
            if (!MatchAny(jump_statements))
                ThrowSyntaxException("Jump Statement expected");

            if (Match(TokenType.BREAK_KEYWORD) || Match(TokenType.CONTINUE_KEYWORD))
            {
                return new JumpStatementNode(ConsumeToken(), null);
            }
            else
            {
                var token = ConsumeToken();
                var expr = OptionalExpression();
                return new JumpStatementNode(token, expr);
            }
        }
    }
}