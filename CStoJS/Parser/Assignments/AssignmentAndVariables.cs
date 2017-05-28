using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void AssignmentOptions(ref List<EnumNode> identifier, ref EnumNode actual)
        {
            printDebug("Assignment Options");
            if (!Match(TokenType.OP_ASSIGN))
            {
                OptionalAssignableIdentifiersListPrime(ref identifier);
            }
            else
            {
                MatchExactly(new TokenType[] { TokenType.OP_ASSIGN });
                actual.assignment = new ExpressionNode();
                Expression();
                OptionalAssignableIdentifiersListPrime(ref identifier);
            }
        }

        private void OptionalVariableInitializerList()
        {
            printDebug("Optional Variable Initializer List ==TODO");
            if (MatchAny(this.expression_operators))
            {
                VariableInitializerList();
            }
            else
            {
                //epsilon
            }
        }

        private void VariableInitializerList()
        {
            VariableInitializer();
            VariableInitializerPrime();
        }

        private void VariableInitializerPrime()
        {
            if (ConsumeOnMatch(TokenType.COMMA))
            {
                VariableInitializerList();
            }
            else
            {
                //epsilomn
            }
        }

        void VariableAssigner(ref FieldNode field)
        {
            printDebug("Variable Assigner");
            if (Match(TokenType.OP_ASSIGN))
            {
                ConsumeToken();
                VariableInitializer();
            }
            else
            {
                //EPSILON
            }
        }
        List<FieldNode> VariableDeclaratorListPrime(Token encap, Token modifier, TypeDeclarationNode type)
        {
            printDebug("Variable Declarator List Prime");
            if (Match(TokenType.COMMA))
            {
                ConsumeToken();
                return VariableDeclaratorList(encap, modifier, type);
            }
            else
            {
                //EPSILON
                return new List<FieldNode>();
            }
        }

        void VariableInitializer()
        {
            printDebug("Variable Initializer");
            //Change this after
            if (MatchAny(this.expression_operators))
            {
                Expression();
            }
            else if (Match(TokenType.BRACE_OPEN))
            {
                ArrayInitializer();
            }
            else
            {
                ThrowSyntaxException("VariableInitializer expected");
            }

        }

        List<FieldNode> VariableDeclaratorList(Token encap, Token modifier, TypeDeclarationNode type)
        {
            printDebug("Variable Declarator List");
            var token = MatchExactly(TokenType.ID );
            var field = new FieldNode(type, new IdentifierNode(token), new EncapsulationNode(encap), modifier);
            
            VariableAssigner(ref field);
            
            var fields = VariableDeclaratorListPrime(encap, modifier, type);

            fields.Insert(0, field);
            return fields;
        }
    }
}