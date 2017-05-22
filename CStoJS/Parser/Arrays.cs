using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void ArrayInitializer(){
            printDebug("Array Initializer");
            MatchExactly(TokenType.BRACE_OPEN);
            OptionalVariableInitializerList();
            MatchExactly(TokenType.BRACE_CLOSE);
        }

        private void OptionalArrayAccessList()
        {
            printDebug("Optional Array Access List");
            if(ConsumeOnMatch(TokenType.BRACKET_OPEN)){
                ExpressionList();
                MatchExactly(TokenType.BRACKET_CLOSE);
                OptionalArrayAccessList();
            }else{
                //epsilon
            }
        }

        private void RankSpecifierList()
        {
            printDebug("Rank Specifier List");
            RankSpecifier();
            OptionalRankSpecifierList();
        }

        private void RankSpecifier()
        {
            printDebug("Rank Specifier");
            MatchExactly(TokenType.BRACKET_OPEN);
            OptionalCommaList();
            MatchExactly(TokenType.BRACKET_CLOSE);
        }

        private void OptionalCommaList()
        {
            printDebug("Optional Comma List");
            if( Match(TokenType.COMMA) ){
                CommaList();
            }else{
                //epsiloon
            }
        }

        private void CommaList()
        {
            printDebug("Comma List");
            MatchExactly(TokenType.COMMA);
            OptionalCommaList();
        }

        private void OptionalRankSpecifierList(){
            printDebug("Optional Rank Specifier List");
            if (Match(TokenType.BRACKET_OPEN)){
                RankSpecifierList();
            }else{
                //epsilon
            }
        }

        private void OptionalArrayInitializer(){
            printDebug("Optional Array Initializer");
            if(Match(TokenType.BRACE_OPEN)){
                ArrayInitializer();
            }else{
                //epsilon
            }
        }
    }
}