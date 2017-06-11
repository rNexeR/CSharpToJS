using CStoJS.LexerLibraries;
using CStoJS.Exceptions;
using CStoJS.Inputs;
using System.Linq;
using System;
using CStoJS.Tree;
using System.Collections.Generic;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        NamespaceNode CompilationUnit()
        {
            printDebug("Compilation Unit");
            var compi_unit = new NamespaceNode();
            // var using_nodes = new List<UsingNode>();
            // var namespaces_nodes = new List<NamespaceNode>();
            // var types_nodes = new List<TypeDeclarationNode>();
            // if( MatchAny( new TokenType[]{TokenType.USING_KEYWORD} ) ){
            OptionalUsingDirective(ref compi_unit);
            OptionalNamespaceMemberDeclaration(ref compi_unit);
            // }else if( MatchAny( new TokenType[]{ TokenType.NAMESPACE_KEYWORD }) ){
            //     OptionalNamespaceMemberDeclaration();
            // }
            // if( MatchAny( this.class_modifiers.Concat(this.encapsulation_modifiers).Concat(new TokenType[]{TokenType.CLASS_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.INTERFACE_KEYWORD }).ToArray() ) ){
            //     TypeDeclarationList();
            // }
            // }else{
            //     // ThrowSyntaxException("Using Directive, Namespace Declaration or Type Declaration Expected");
            //     //epsilon
            // }
            return compi_unit;
        }

        void OptionalUsingDirective(ref NamespaceNode namespace_node)
        {
            printDebug("Optional Using Directive");
            if (MatchAny(new TokenType[] { TokenType.USING_KEYWORD }))
            {
                UsingDirective(ref namespace_node);
            }
            else
            {
                //EPSILON

            }
        }

        void OptionalNamespaceMemberDeclaration(ref NamespaceNode namespace_node)
        {
            printDebug("Optional Namespace Member Declaration");
            if (MatchAny(this.encapsulation_modifiers.Concat(new TokenType[] { TokenType.NAMESPACE_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.CLASS_KEYWORD, TokenType.INTERFACE_KEYWORD, TokenType.ABSTRACT_KEYWORD }).ToArray()))
            {
                NamespaceMemberDeclaration(ref namespace_node);
            }
            else
            {
                //EPSILON
            }
        }

        void UsingDirective(ref NamespaceNode namespace_node)
        {
            printDebug("Using Directive");
            
            var identifier = new List<Token>();
            var tokens = MatchExactly(new TokenType[] { TokenType.USING_KEYWORD, TokenType.ID });
            
            identifier.Add(tokens[1]);
            IdentifierAttribute(ref identifier);
            
            MatchExactly(new TokenType[] { TokenType.END_STATEMENT });
            
            namespace_node.using_array.Add(new UsingNode(new IdentifierNode(identifier)));
            
            OptionalUsingDirective(ref namespace_node);
        }

        void NamespaceMemberDeclaration(ref NamespaceNode namespace_node)
        {
            printDebug("Namespace Member Declaration");
            if (Match(TokenType.NAMESPACE_KEYWORD))
            {
                var new_namespace = new NamespaceNode();
                // new_namespace.using_array.AddRange(namespace_node.using_array);
                // new_namespace.using_array.Add(new UsingNode(namespace_node.identifier));
                // var new_using_nodes = new List<UsingNode>();
                // var new_namespaces_nodes = new List<NamespaceNode>();
                // var new_types_nodes = new List<TypeDeclarationNode>();
                // new_namespace.namespace_node = new_namespaces_nodes;
                // new_namespace.using_node = new_using_nodes;
                // new_namespace.types_declaration_node = new_types_nodes;
                
                NamespaceDeclaration(ref new_namespace);
                OptionalNamespaceMemberDeclaration(ref namespace_node);
                
                namespace_node.namespace_array.Insert(0,new_namespace);
            }
            else
            {
                namespace_node.types_declaration_array.AddRange(TypeDeclarationList());
                OptionalNamespaceMemberDeclaration(ref namespace_node);
            }
        }

        void NamespaceDeclaration(ref NamespaceNode namespace_node)
        {
            printDebug("Namespace Declaration");
            var identifier = new List<Token>();
            var p_identifier = MatchExactly(new TokenType[] { TokenType.NAMESPACE_KEYWORD, TokenType.ID })[1];
            
            identifier.Add(p_identifier);
            IdentifierAttribute(ref identifier);
            
            NamespaceBody(ref namespace_node);
            namespace_node.identifier = new IdentifierNode(identifier);
        }

        void NamespaceBody(ref NamespaceNode namespace_node)
        {
            printDebug("Namespace Body");
            var using_nodes = new List<UsingNode>();
            MatchExactly(new TokenType[] { TokenType.BRACE_OPEN });
            
            OptionalUsingDirective(ref namespace_node);
            OptionalNamespaceMemberDeclaration(ref namespace_node);
            
            if (MatchAny(this.class_modifiers.Concat(this.encapsulation_modifiers).Concat(new TokenType[] { TokenType.CLASS_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.INTERFACE_KEYWORD }).ToArray()))
            {
                namespace_node.types_declaration_array.AddRange(TypeDeclarationList());
            }
            MatchExactly(new TokenType[] { TokenType.BRACE_CLOSE });
        }

        void IdentifierAttribute(ref List<Token> identifiers)
        {
            printDebug("Identifier Attribute");
            if (Match(TokenType.OP_MEMBER_ACCESS))
            {
                var tokens = MatchExactly(new TokenType[] { TokenType.OP_MEMBER_ACCESS, TokenType.ID });
                identifiers.Add(tokens[1]);
                IdentifierAttribute(ref identifiers);
            }
            else
            {

            }
        }

        void IdentifierAttributeLA()
        {
            printDebug("Identifier Attribute LA");
            if (ConsumeOnMatchLA(TokenType.OP_MEMBER_ACCESS) && ConsumeOnMatchLA(TokenType.ID))
            {
                IdentifierAttributeLA();
            }
            else
            {

            }
        }
    }
}