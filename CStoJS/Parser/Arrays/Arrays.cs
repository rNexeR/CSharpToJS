using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;
using CStoJS.Tree;
using System.Collections.Generic;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private ExpressionNode FuncOrArrayCall(ref ExpressionNode left)
        {
            printDebug("Optional Func Or Array Call");
            if (ConsumeOnMatch(TokenType.PAREN_OPEN))
            {
                var args = ArgumentList();
                MatchExactly(TokenType.PAREN_CLOSE);
                return new FunctionCallExpressionNode(left, args);
            }
            else if (Match(TokenType.BRACKET_OPEN))
            {
                return new ArrayAccessExpressionNode(OptionalArrayAccessList());
            }
            else
            {
                ThrowSyntaxException("Function call or Array Access expected");
                return null;
            }
        }

        private List<ArrayAccessNode> OptionalArrayAccessList()
        {
            printDebug("Optional Array Access List");
            if (ConsumeOnMatch(TokenType.BRACKET_OPEN))
            {
                var exprs = ExpressionList();
                MatchExactly(TokenType.BRACKET_CLOSE);
                var access = new ArrayAccessNode(exprs);
                var lista = OptionalArrayAccessList();
                lista.Insert(0, access);
                return lista;
            }
            else
            {
                return new List<ArrayAccessNode>();
            }
        }

        private ArrayInitializerNode ArrayInitializer()
        {
            printDebug("Optional Array Access List");
            MatchExactly(TokenType.BRACE_OPEN);
            var initializers = OptionalVariableInitializerList();
            MatchExactly(TokenType.BRACE_CLOSE);
            return new ArrayInitializerNode(initializers);
        }

        private void RankSpecifier(ref ArrayType arr)
        {
            printDebug("Rank Specifier");
            OptionalCommaList(ref arr);
            arr.arrayOfArrays++;
            MatchExactly(TokenType.BRACKET_CLOSE);
        }

        private void RankSpecifierList(ref ArrayType arr)
        {
            printDebug("Rank Specifier List");
            RankSpecifier(ref arr);
            OptionalRankSpecifierList(ref arr);
        }

        private void OptionalRankSpecifierList(ref ArrayType arr)
        {
            printDebug("Optional Rank Specifier List");
            if(ConsumeOnMatch(TokenType.BRACKET_OPEN)){
                RankSpecifierList(ref arr);
            }else{
                //epsilon
            }
        }

        private ArrayInitializerNode OptionalArrayInitializer(){
            if(Match(TokenType.BRACE_OPEN)){
                return ArrayInitializer();
            }else{
                return null;
            }
        }

        private void OptionalCommaList(ref ArrayType arr)
        {
            printDebug("Optional Comma List");
            if (Match(TokenType.COMMA))
            {
                CommaList(ref arr);
            }
            else
            {
                //epsiloon
            }
        }

        private void CommaList(ref ArrayType arr)
        {
            printDebug("Comma List");
            MatchExactly(TokenType.COMMA);
            arr.dimensions++;
            OptionalCommaList(ref arr);
        }
    }
}