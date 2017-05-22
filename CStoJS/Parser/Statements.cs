using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;
using System.Linq;
using System;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private TokenType[] jump_statements = new TokenType[]{TokenType.RETURN_KEYWORD, TokenType.BREAK_KEYWORD, TokenType.CONTINUE_KEYWORD};
        private TokenType[] selection_statements = new TokenType[]{TokenType.IF_KEYWORD, TokenType.SWITCH_KEYWORD};
        private TokenType[] iteration_statements = new TokenType[]{TokenType.WHILE_KEYWORD, TokenType.DO_KEYWORD, TokenType.FOREACH_KEYWORD};
        void OptionalStatementList()
        {
            printDebug("Optional Statement List");
            return;
            if (true)
            {
                StatementList();
            }
            else
            {

            }
        }

        private void StatementList()
        {
            Statement();
            OptionalStatementList();
        }

        private void Statement()
        {
            if (1 == 1)
            {
                LocalVariableDeclaration();
                MatchExactly(TokenType.END_STATEMENT);
            }
            else if (MatchAny( new TokenType[]{TokenType.BRACE_OPEN, TokenType.END_STATEMENT}) || MatchAny(selection_statements) || MatchAny(iteration_statements) || MatchAny(jump_statements))
            {
                EmbeddedStatement();
            }
            else
            {
                ThrowSyntaxException("Local Variable Declaration or Embedded Statement expected");
            }
        }

        private void EmbeddedStatement()
        {
            if(!MatchAny(new TokenType[]{TokenType.BRACE_OPEN, TokenType.END_STATEMENT}) || !MatchAny(selection_statements.Concat(iteration_statements).Concat(jump_statements).ToArray()))
                ThrowSyntaxException("Maybe Empty Block, Statement Expression, Selection Statement, Iteration Statement or Jump Statement expected");

            if(MatchAny( new TokenType[]{TokenType.BRACE_OPEN, TokenType.END_STATEMENT} )){
                MaybeEmptyBlock();
            }else if(1 == 1){
                StatementExpression();
                MatchExactly(TokenType.END_STATEMENT);
            }else if(MatchAny(selection_statements)){
                SelectionStateMent();
            }else if(MatchAny(iteration_statements)){
                IterationStatement();
            }else if(MatchAny(jump_statements)){
                JumpStatement();
                MatchExactly(TokenType.END_STATEMENT);
            }
        }

        private void SelectionStateMent()
        {
            throw new NotImplementedException();
        }

        private void StatementExpression()
        {
            throw new NotImplementedException();
        }

        private void LocalVariableDeclaration()
        {
            throw new NotImplementedException();
        }
    }
}