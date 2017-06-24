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
        private StatementNode SelectionStateMent()
        {
            printDebug("Selection Statement");

            if(!MatchAny(selection_statements))
                ThrowSyntaxException("Conditional Statement expected");

            if(Match(TokenType.IF_KEYWORD)){
                return IfStatement();
            }else{
                return SwitchStatement();
            }
        }

        private StatementNode SwitchStatement()
        {
            printDebug("Switch Statement");
            MatchExactly(new TokenType[]{TokenType.SWITCH_KEYWORD, TokenType.PAREN_OPEN});
            var constantExpression = Expression();
            MatchExactly(new TokenType[]{TokenType.PAREN_CLOSE, TokenType.BRACE_OPEN});
            var cases = OptionalSwitchSectionList();
            MatchExactly(TokenType.BRACE_CLOSE);
            return new SwitchStatementNode(constantExpression, cases);
        }

        private List<CaseExpressionNode> OptionalSwitchSectionList()
        {
            printDebug("Optional Switch Section List");
            if(MatchAny(new TokenType[]{TokenType.CASE_KEYWORD, TokenType.DEFAULT_KEYWORD})){
                var toCompare = SwitchLabelList();
                var body = StatementList();
                var otherCases = OptionalSwitchSectionList();

                otherCases.Insert(0, new CaseExpressionNode(toCompare, body));
                return otherCases;

            }else{
                return new List<CaseExpressionNode>();
            }
        }

        private List<CaseNode> SwitchLabelList()
        {
            printDebug("Switch Label List");
            var label = SwitchLabel();
            MatchExactly(TokenType.OP_HIERARCHY);
            var otherLabels = SwitchLabelListPrime();
            
            otherLabels.Insert(0, label);
            return otherLabels;
        }

        private List<CaseNode> SwitchLabelListPrime()
        {
            printDebug("Switch Label List Prime");
            if(MatchAny(new TokenType[]{TokenType.CASE_KEYWORD, TokenType.DEFAULT_KEYWORD})){
                return SwitchLabelList();
            }else{
                return new List<CaseNode>();
            }
        }

        private CaseNode SwitchLabel()
        {
            printDebug("Switch Label");
            if(Match(TokenType.CASE_KEYWORD)){
                var token = MatchExactly(TokenType.CASE_KEYWORD);
                var expr = Expression();
                return new CaseNode(token, expr);
            }else{
                var token = MatchExactly(TokenType.DEFAULT_KEYWORD);
                return new CaseNode(token, null);
            }
        }

        private StatementNode IfStatement()
        {
            printDebug("If Statement");
            MatchExactly(new TokenType[]{TokenType.IF_KEYWORD, TokenType.PAREN_OPEN});
            var condition = Expression();
            MatchExactly(TokenType.PAREN_CLOSE);
            var body = EmbeddedStatement();
            var elsePart = OptionalElsePart();

            return new IfStatementNode(condition, body, elsePart);
        }

        private ElseNode OptionalElsePart()
        {
            printDebug("Optional Else Part");
            if(Match(TokenType.ELSE_KEYWORD)){
                return ElsePart();
            }else{
                return null;
            }
        }

        private ElseNode ElsePart()
        {
            printDebug("Else Part");
            MatchExactly(TokenType.ELSE_KEYWORD);
            return new ElseNode(EmbeddedStatement() as EmbeddedStatementNode);
        }
    }
}