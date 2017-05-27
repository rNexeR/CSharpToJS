using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;
using System.Linq;
using System;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private void SelectionStateMent()
        {
            printDebug("Selection Statement");

            if(!MatchAny(selection_statements))
                ThrowSyntaxException("Conditional Statement expected");

            if(Match(TokenType.IF_KEYWORD)){
                IfStatement();
            }else{
                SwitchStatement();
            }
        }

        private void SwitchStatement()
        {
            printDebug("Switch Statement");
            MatchExactly(new TokenType[]{TokenType.SWITCH_KEYWORD, TokenType.PAREN_OPEN});
            Expression();
            MatchExactly(new TokenType[]{TokenType.PAREN_CLOSE, TokenType.BRACE_OPEN});
            OptionalSwitchSectionList();
            MatchExactly(TokenType.BRACE_CLOSE);
        }

        private void OptionalSwitchSectionList()
        {
            printDebug("Optional Switch Section List");
            if(MatchAny(new TokenType[]{TokenType.CASE_KEYWORD, TokenType.DEFAULT_KEYWORD})){
                SwitchLabelList();
                StatementList();
                OptionalSwitchSectionList();
            }else{
                //epsilon
            }
        }

        private void SwitchLabelList()
        {
            printDebug("Switch Label List");
            SwitchLabel();
            MatchExactly(TokenType.OP_HIERARCHY);
            SwitchLabelListPrime();
        }

        private void SwitchLabelListPrime()
        {
            printDebug("Switch Label List Prime");
            if(MatchAny(new TokenType[]{TokenType.CASE_KEYWORD, TokenType.DEFAULT_KEYWORD})){
                SwitchLabelList();
            }else{
                //epsilon
            }
        }

        private void SwitchLabel()
        {
            printDebug("Switch Label");
            if(ConsumeOnMatch(TokenType.CASE_KEYWORD)){
                Expression();
            }else{
                MatchExactly(TokenType.DEFAULT_KEYWORD);
            }
        }

        private void IfStatement()
        {
            printDebug("If Statement");
            MatchExactly(new TokenType[]{TokenType.IF_KEYWORD, TokenType.PAREN_OPEN});
            Expression();
            MatchExactly(TokenType.PAREN_CLOSE);
            EmbeddedStatement();
            OptionalElsePart();
        }

        private void OptionalElsePart()
        {
            printDebug("Optional Else Part");
            if(Match(TokenType.ELSE_KEYWORD)){
                ElsePart();
            }else{
                //epsilon
            }
        }

        private void ElsePart()
        {
            printDebug("Else Part");
            MatchExactly(TokenType.ELSE_KEYWORD);
            EmbeddedStatement();
        }
    }
}