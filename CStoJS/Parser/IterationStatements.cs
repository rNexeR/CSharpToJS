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
            throw new NotImplementedException();
        }

        private void ForStatement()
        {
            throw new NotImplementedException();
        }

        private void DoStatement()
        {
            throw new NotImplementedException();
        }

        private void WhileStatement()
        {
            throw new NotImplementedException();
        }

        private void JumpStatement()
        {
            if(!MatchAny(jump_statements))
                ThrowSyntaxException("Jump Statement expected");
            
            if( ConsumeOnMatch(TokenType.BREAK_KEYWORD) || ConsumeOnMatch(TokenType.CONTINUE_KEYWORD) ){
                MatchExactly(TokenType.END_STATEMENT);
            }else if(ConsumeOnMatch(TokenType.RETURN_KEYWORD)){
                if(!ConsumeOnMatch(TokenType.END_STATEMENT)){
                    OptionalExpressionList();
                    MatchExactly(TokenType.END_STATEMENT);
                }
            }
        }
    }
}