using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;
using System.Linq;
using System;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void OptionalStatementList()
        {
            printDebug("Optional Statement List");
            TokenType[] nuevo = new TokenType[]{
                TokenType.VAR_KEYWORD, TokenType.BRACE_OPEN, TokenType.END_STATEMENT
            }.Concat(iteration_statements).Concat(selection_statements).Concat(jump_statements).ToArray();
            if (MatchAny(nuevo.Concat(unary_operators).Concat(types).Concat(literals).Concat(unary_expression_options).ToArray()))
            {
                StatementList();
            }
            else
            {
                //epsilon
            }
        }

        private void StatementList()
        {
            printDebug("Statement List");
            Statement();
            OptionalStatementList();
        }

        private void Statement()
        {
            printDebug("Statement");
            TokenType[] embedded= new TokenType[]{
                TokenType.BRACE_OPEN,TokenType.END_STATEMENT
            }.Concat(iteration_statements).Concat(selection_statements).Concat(jump_statements).ToArray();

            if ( MatchAndComsumeAnyLA( this.types.Concat(new TokenType[]{TokenType.VAR_KEYWORD}).ToArray() ) &&
                ConsumeOnMatchLA(TokenType.ID)
             )
            {
                // lookAhead = new Token[]{};
                RollbackLA();
                LocalVariableDeclaration();
                MatchExactly(TokenType.END_STATEMENT);
            }
            else{
                if(lookAhead.Length > 0 && lookAhead[0].type == TokenType.ID)
                        RollbackLA();
                printDebug("==> Llego aqui");
                if (MatchAny( embedded.Concat(unary_expression_options).Concat(unary_operators).
                Concat(literals).ToArray() ) )
                {
                    EmbeddedStatement();
                }
                else
                {
                    ThrowSyntaxException("Local Variable Declaration or Embedded Statement expected");
                }
            }
        }

        private void EmbeddedStatement()
        {
            printDebug("Embedded Statement");
            if(!MatchAny(new TokenType[]{TokenType.BRACE_OPEN, TokenType.END_STATEMENT}.Concat(selection_statements).Concat(iteration_statements).Concat(jump_statements).Concat(unary_operators).Concat(literals).Concat(unary_expression_options).ToArray()) )
                ThrowSyntaxException("Maybe Empty Block, Statement Expression, Selection Statement, Iteration Statement or Jump Statement expected");

            if(MatchAny( new TokenType[]{TokenType.BRACE_OPEN, TokenType.END_STATEMENT} )){
                MaybeEmptyBlock();
            }else if( MatchAny(unary_operators.Concat(literals).Concat(unary_expression_options).ToArray()) ){
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

        private void StatementExpression()
        {
            printDebug("Statement Expression");
            UnaryExpression();
            StatementExpressionFactorized();
        }

        private void StatementExpressionFactorized()
        {
            if(MatchAndComsumeAny(assignment_operators)){
                Expression();
                StatementExpressionPrime();
            }else{
                StatementExpressionPrime();
            }
        }

        private void StatementExpressionPrime()
        {
            if(ConsumeOnMatch(TokenType.PAREN_OPEN)){
                ArgumentList();
                MatchExactly(TokenType.PAREN_CLOSE);   
            }else if(MatchAny(increment_decrement_operators)){
                IncrementDecrement();
            }else{
                //epsilon
            }
        }

        private void LocalVariableDeclaration()
        {
            printDebug("Local Variable Declaration");
            if(!MatchAny(this.types.Concat(new TokenType[]{ TokenType.VAR_KEYWORD }).ToArray())){
                ThrowSyntaxException("Type or Var expected");
            }

            if(!ConsumeOnMatch(TokenType.VAR_KEYWORD)){
                Type();
            }
            VariableDeclaratorList();
        }

        private void OptionalStatementExpressionList()
        {
            printDebug("Optional Statement Expression List");
            if (MatchAny(unary_expression_options.Concat(unary_operators).Concat(literals).ToArray()))
            {
                StatementExpressionList();
            }
            else
            {
                //epsilon
            }
        }

        private void StatementExpressionList()
        {
            printDebug("Statement Expression List");
             TokenType[] nuevo = new TokenType[]{
                TokenType.VAR_KEYWORD, TokenType.BRACE_OPEN, TokenType.END_STATEMENT
            }.Concat(iteration_statements).Concat(selection_statements).Concat(jump_statements).ToArray();
            if (MatchAny(nuevo.Concat(types).Concat(unary_expression_options).Concat(unary_operators).
                Concat(literals).ToArray()
                ))
            {
                StatementList();
            }
            else
            {
                //epsilon
            }
        }
    }
}