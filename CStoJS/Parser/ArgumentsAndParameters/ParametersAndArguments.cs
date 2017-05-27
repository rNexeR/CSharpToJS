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
        private List<TypeDeclarationNode> FixedParameters()
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
                return new List<TypeDeclarationNode>();
            }
        }

        private TypeDeclarationNode FixedParameter()
        {
            printDebug("Fixed Parameter");

            var parameter = Type();

            var token = MatchExactly(TokenType.ID );
            parameter.identifier = new IdentifierNode(token);

            return parameter;
        }

        private List<TypeDeclarationNode> FixedParametersPrime()
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
                return new List<TypeDeclarationNode>();
            }

        }

        void ArgumentList()
        {
            printDebug("Argument List");
            if (MatchAny(this.expression_operators) /* Is it a expression? */ )
            {
                Expression();
                ArgumentListPrime();
            }
            else
            {
                //EPSILON
            }
        }

        void ArgumentListPrime()
        {
            printDebug("Argument List Prime");
            if (ConsumeOnMatch(TokenType.COMMA))
            {
                Expression();
                ArgumentListPrime();
            }
            else
            {
                //EPSION
            }
        }

    }
}