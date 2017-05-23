using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {

        private void OPtionalFuncOrArrayCall()
        {
            printDebug("Optional Func Or Array Call");
            if (ConsumeOnMatch(TokenType.PAREN_OPEN))
            {
                ArgumentList();
                MatchExactly(TokenType.PAREN_CLOSE);
            }
            else if (Match(TokenType.BRACKET_OPEN))
            {
                OptionalArrayAccessList();
            }
            else
            {
                //epsilon
            }
        }

        private void OptionalArrayAccessList()
        {
            printDebug("Optional Array Access List");
            if (ConsumeOnMatch(TokenType.BRACKET_OPEN))
            {
                ExpressionList();
                MatchExactly(TokenType.BRACKET_CLOSE);
                OptionalArrayAccessList();
            }
            else
            {
                //epsilon
            }
        }

        private void ArrayInitializer()
        {
            printDebug("Optional Array Access List");
            MatchExactly(TokenType.BRACE_OPEN);
            OptionalVariableInitializerList();
            MatchExactly(TokenType.BRACE_CLOSE);
        }

        private void RankSpecifier()
        {
            printDebug("Rank Specifier");
            OptionalCommaList();
            MatchExactly(TokenType.BRACKET_CLOSE);
        }

        private void RankSpecifierList()
        {
            printDebug("Rank Specifier List");
            RankSpecifier();
            OptionalRankSpecifierList();
        }

        private void OptionalRankSpecifierList()
        {
            printDebug("Optional Rank Specifier List");
            if(ConsumeOnMatch(TokenType.BRACKET_OPEN)){
                RankSpecifierList();
            }else{
                //epsilon
            }
        }

        private void OptionalArrayInitializer(){
            if(Match(TokenType.BRACE_OPEN)){
                ArrayInitializer();
            }else{
                //epsilon
            }
        }

        private void OptionalCommaList()
        {
            printDebug("Optional Comma List");
            if (Match(TokenType.COMMA))
            {
                CommaList();
            }
            else
            {
                //epsiloon
            }
        }

        private void CommaList()
        {
            printDebug("Comma List");
            MatchExactly(TokenType.COMMA);
            OptionalCommaList();
        }
    }
}