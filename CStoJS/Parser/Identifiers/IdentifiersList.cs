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

        List<IdentifierNode> IdentifierList()
        {
            // var identifier = new List<IdentifierNode>();
            printDebug("Identifier List");
            
            var tokens = new List<Token>();
            var token = MatchExactly(TokenType.ID);
            
            tokens.Add(token);
            IdentifierAttribute(ref tokens);
            
            var inherit = IdentifierListPrime();
            inherit.Add(new IdentifierNode(tokens));
            return inherit;

        }

        List<IdentifierNode> OptionalIdentifierList()
        {
            printDebug("Optional Identifier List");
            if (Match(TokenType.ID))
            {
                return IdentifierList();
            }
            else
            {
                return new List<IdentifierNode>();
            }
        }

        List<IdentifierNode> IdentifierListPrime()
        {
            printDebug("Identifier List Prime");
            if (Match(TokenType.COMMA))
            {
                var tokens = new List<Token>();
                var tokens_t = MatchExactly(new TokenType[] { TokenType.COMMA, TokenType.ID });
                tokens.Add(tokens_t[1]);
                
                IdentifierAttribute(ref tokens);

                var identifiers = IdentifierListPrime();
                identifiers.Add(new IdentifierNode(tokens));

                return identifiers;
            }
            else
            {
                return new List<IdentifierNode>();
            }
        }
    }
}