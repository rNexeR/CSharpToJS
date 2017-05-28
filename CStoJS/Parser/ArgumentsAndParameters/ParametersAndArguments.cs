using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        private List<ParameterNode> FixedParameters()
        {
            printDebug("Fixed Parameters");
            if (MatchAny(this.types))
            {
                var parameter = FixedParameter();
                var parameters = FixedParametersPrime();

                parameters.Insert(0, parameter);
                return parameters;
            }
            else
            {
                //EPSILON 
                return new List<ParameterNode>();
            }
        }

        private ParameterNode FixedParameter()
        {
            printDebug("Fixed Parameter");
            var parameter_type = Type();

            var token = MatchExactly(TokenType.ID );
            var identifier = new IdentifierNode(token);

            return new ParameterNode(parameter_type, identifier);
        }

        private List<ParameterNode> FixedParametersPrime()
        {
            printDebug("Fixed ParametersPrime");
            if (Match(TokenType.COMMA))
            {
                MatchExactly(new TokenType[] { TokenType.COMMA });
                var parameter = FixedParameter();
                var parameters = FixedParametersPrime();
                parameters.Insert(0, parameter);

                return parameters;
            }
            else
            {
                //EPSILON
                return new List<ParameterNode>();
            }

        }

        List<ArgumentNode> ArgumentList()
        {
            printDebug("Argument List");
            if (MatchAny(this.expression_operators) /* Is it a expression? */ )
            {
                var arg = Expression();
                var args = ArgumentListPrime();
                args.Insert(0, new ArgumentNode(arg));
                return args;
            }
            else
            {
                return new List<ArgumentNode>();
            }
        }

        List<ArgumentNode> ArgumentListPrime()
        {
            printDebug("Argument List Prime");
            if (ConsumeOnMatch(TokenType.COMMA))
            {
                var arg = Expression();
                var args = ArgumentListPrime();
                args.Insert(0, new ArgumentNode(arg));
                return args;
            }
            else
            {
                return new List<ArgumentNode>();
            }
        }

    }
}