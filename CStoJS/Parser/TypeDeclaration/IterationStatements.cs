using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;
using System.Linq;
using System;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private void IterationStatement()
        {
            printDebug("Iteration Statement");
            if(!MatchAny(iteration_statements))
                ThrowSyntaxException("Iteration Statement expected");

            if(Match(TokenType.WHILE_KEYWORD)){
                WhileStatement();
            }else if(Match(TokenType.DO_KEYWORD)){
                DoStatement();
            }else if(Match(TokenType.FOR_KEYWORD)){
                ForStatement();
            }else{  
                ForEachStatement();
            }
        }

        private void ForEachStatement()
        {
            printDebug("ForEach Statement");
            MatchExactly(TokenType.FOREACH_KEYWORD);
            MatchExactly(TokenType.PAREN_OPEN);
            TypeOrVar();
            MatchExactly(TokenType.ID);
            MatchExactly(TokenType.IN_KEYWORD);
            Expression();
            MatchExactly(TokenType.PAREN_CLOSE);
            EmbeddedStatement();
        }

        private void ForStatement()
        {
            printDebug("For Statement");
            MatchExactly(TokenType.FOR_KEYWORD);
            MatchExactly(TokenType.PAREN_OPEN);
            OptionalForInitializer();
            MatchExactly(TokenType.END_STATEMENT);
            OptionalExpression();
            MatchExactly(TokenType.END_STATEMENT);
            OptionalStatementExpressionList();
            MatchExactly(TokenType.PAREN_CLOSE);
            EmbeddedStatement();
        }

        private void OptionalForInitializer()
        {
            printDebug("Optional For Initializer");
             TokenType[] nuevo = { TokenType.OP_TERNARY, TokenType.OP_HIERARCHY,
                TokenType.OP_NULL_COALESCING, TokenType.OP_CONDITIONAL_OR,
                TokenType.OP_CONDITIONAL_AND, TokenType.OP_BITS_OR,
                TokenType.OP_BITS_XOR, TokenType.OP_BITS_AND, TokenType.ID
            };
            if(MatchAndComsumeAnyLA(types.Concat(new TokenType[]{TokenType.VAR_KEYWORD}).ToArray()) && ConsumeOnMatchLA(TokenType.ID) ){
                RollbackLA();
                LocalVariableDeclaration();
            }else if(MatchAny(nuevo.Concat(expression_operators).ToArray())){
                RollbackLA();
                StatementExpressionList();
            }else{
                //epsilon
            }
        }

        private void DoStatement()
        {
            printDebug("Do Statement");
            MatchExactly(TokenType.DO_KEYWORD);
            EmbeddedStatement();
            MatchExactly(TokenType.WHILE_KEYWORD);
            MatchExactly(TokenType.PAREN_OPEN);
            Expression();
            MatchExactly(TokenType.PAREN_CLOSE);
            MatchExactly(TokenType.END_STATEMENT);
        }

        private void WhileStatement()
        {
            printDebug("While Statement");
            MatchExactly(TokenType.WHILE_KEYWORD);
            MatchExactly(TokenType.PAREN_OPEN);
            Expression();
            MatchExactly(TokenType.PAREN_CLOSE);
            EmbeddedStatement();
        }

        private void JumpStatement()
        {
            printDebug("Jump Statement");
            if(!MatchAny(jump_statements))
                ThrowSyntaxException("Jump Statement expected");
            
            if( ConsumeOnMatch(TokenType.BREAK_KEYWORD) || ConsumeOnMatch(TokenType.CONTINUE_KEYWORD) ){

            }else if(ConsumeOnMatch(TokenType.RETURN_KEYWORD)){
                    OptionalExpression();
            }
        }
    }
}