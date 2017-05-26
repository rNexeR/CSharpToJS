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
        void OptionalAssignableIdentifiersList(ref List<EnumNode> identifiers)
        {
            printDebug("Optional Assignable Identifiers List");
            
            if (Match(TokenType.ID))
            {
                var token = MatchExactly(TokenType.ID);
                var enum_node = new EnumNode();
                
                AssignmentOptions(ref identifiers, ref enum_node);
                
                enum_node.identifier = new IdentifierNode(token);

                identifiers.Insert(0, enum_node);
            }
            else
            {
                //EPSILON
            }
        }

        void OptionalAssignableIdentifiersListPrime(ref List<EnumNode> identifier)
        {
            printDebug("Optional Assignable Identifiers List Prime");
            if (Match(TokenType.COMMA))
            {
                MatchExactly(TokenType.COMMA);
                OptionalAssignableIdentifiersList(ref identifier);
            }
            else
            {
                //EPSILON
            }
        }

        void IdentifierList()
        {
            printDebug("Identifier List");
            MatchExactly(new TokenType[] { TokenType.ID });
            IdentifierListPrime();
        }

        void OptionalIdentifierList()
        {
            printDebug("Optional Identifier List");
            if (Match(TokenType.ID))
            {
                IdentifierList();
            }
            else
            {
                //EPSILON
            }
        }

        void IdentifierListPrime()
        {
            printDebug("Identifier List Prime");
            if (Match(TokenType.COMMA))
            {
                MatchExactly(new TokenType[] { TokenType.COMMA, TokenType.ID });

                var identifier = new List<Token>();
                IdentifierAttribute(ref identifier);

                IdentifierListPrime();
            }
            else
            {
                //EPSILON
            }
        }
    }
}