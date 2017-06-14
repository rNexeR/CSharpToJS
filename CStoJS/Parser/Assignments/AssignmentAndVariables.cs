using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Collections.Generic;
using CStoJS.Tree;
using System.Linq;

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
                if(!Match(TokenType.LITERAL_INT))
                    ThrowSyntaxException("Int Literal expected.");
                actual.assignment = int.Parse(this.ConsumeToken().lexema);
                OptionalAssignableIdentifiersListPrime(ref identifier);
            }
        }

        private List<VariableInitializer> OptionalVariableInitializerList()
        {
            printDebug("Optional Variable Initializer List ==TODO");
            if (MatchAny(new TokenType[]{TokenType.BRACE_OPEN}.Concat(this.expression_operators).ToArray()))
            {
                return VariableInitializerList();
            }
            else
            {
                return new List<VariableInitializer>();
            }
        }

        private List<VariableInitializer> VariableInitializerList()
        {
            var variable = VariableInitializer();
            var lista = VariableInitializerPrime();
            lista.Insert(0, variable);
            return lista;
        }

        private List<VariableInitializer> VariableInitializerPrime()
        {
            if (ConsumeOnMatch(TokenType.COMMA))
            {
                return VariableInitializerList();
            }
            else
            {
                return new List<VariableInitializer>();
            }
        }

        VariableInitializer VariableAssigner()
        {
            printDebug("Variable Assigner");
            if (Match(TokenType.OP_ASSIGN))
            {
                ConsumeToken();
                return VariableInitializer();
            }
            else
            {
                return null;
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

        VariableInitializer VariableInitializer()
        {
            printDebug("Variable Initializer");
            //Change this after
            if (MatchAny(this.expression_operators))
            {
                return Expression();
            }
            else if (Match(TokenType.BRACE_OPEN))
            {
                return ArrayInitializer();
            }
            else
            {
                ThrowSyntaxException("VariableInitializer expected");
                return null;
            }

        }

        List<FieldNode> VariableDeclaratorList(Token encap, Token modifier, TypeDeclarationNode type)
        {
            printDebug("Variable Declarator List");
            var token = MatchExactly(TokenType.ID );
            var field = new FieldNode(type, new IdentifierNode(token), new EncapsulationNode(encap), modifier);
            
            field.assignment = VariableAssigner();
            
            var fields = VariableDeclaratorListPrime(encap, modifier, type);

            fields.Insert(0, field);
            return fields;
        }
    }
}