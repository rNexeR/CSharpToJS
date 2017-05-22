using CStoJS.LexerLibraries;
using CStoJS.Exceptions;
using CStoJS.Inputs;
using System.Linq;
using System;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        void CompilationUnit(){
            printDebug("Compilation Unit");
            if( MatchAny( new TokenType[]{TokenType.USING_KEYWORD} ) ){
                OptionalUsingDirective();
                OptionalNamespaceMemberDeclaration();
            }else if( MatchAny( new TokenType[]{ TokenType.NAMESPACE_KEYWORD }) ){
                OptionalNamespaceMemberDeclaration();
            }
            else if( MatchAny( this.class_modifiers.Concat(this.encapsulation_modifiers).Concat(new TokenType[]{TokenType.CLASS_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.INTERFACE_KEYWORD }).ToArray() ) ){
                TypeDeclarationList();
            }else{
                // ThrowSyntaxException("Using Directive, Namespace Declaration or Type Declaration Expected");
                //epsilon
            }
        }

        void OptionalUsingDirective(){
            printDebug("Optional Using Directive");
            if( MatchAny( new TokenType[]{ TokenType.USING_KEYWORD }) ){
                UsingDirective();
            }else{
                //EPSILON
            }
        }

        void OptionalNamespaceMemberDeclaration(){
            printDebug("Optional Namespace Member Declaration");
            if( MatchAny( this.encapsulation_modifiers.Concat(new TokenType[]{TokenType.NAMESPACE_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.CLASS_KEYWORD, TokenType.INTERFACE_KEYWORD, TokenType.ABSTRACT_KEYWORD}).ToArray() ) ){
                NamespaceMemberDeclaration();
            }else{
                //EPSILON
            }
        }

        void UsingDirective(){
            printDebug("Using Directive");
            MatchExactly( new TokenType[]{TokenType.USING_KEYWORD, TokenType.ID});
            IdentifierAttribute();
            MatchExactly( new TokenType[]{TokenType.END_STATEMENT} );
            OptionalUsingDirective();
        }

        void NamespaceMemberDeclaration(){
            printDebug("Namespace Member Declaration");
             if( Match( TokenType.NAMESPACE_KEYWORD ) ){
               NamespaceDeclaration();
               OptionalNamespaceMemberDeclaration();
            }else{
                TypeDeclarationList();
                OptionalNamespaceMemberDeclaration();
            }
        }

        void NamespaceDeclaration(){
            printDebug("Namespace Declaration");
            MatchExactly( new TokenType[]{TokenType.NAMESPACE_KEYWORD, TokenType.ID} );
            IdentifierAttribute();
            NamespaceBody();
        }

        void NamespaceBody(){
            printDebug("Namespace Body");
            MatchExactly( new TokenType[]{TokenType.BRACE_OPEN} );
            OptionalUsingDirective();
            OptionalNamespaceMemberDeclaration();
            MatchExactly( new TokenType[]{TokenType.BRACE_CLOSE} );
        }

        void IdentifierAttribute(){
            printDebug("Identifier Attribute");
            if ( OptionalMatchExactly( new TokenType[]{ TokenType.OP_MEMBER_ACCESS, TokenType.ID } ) ){
                IdentifierAttribute();
            }else{

            }
        }

        void IdentifierAttributeLA(){
            printDebug("Identifier Attribute LA");
            if ( ConsumeOnMatchLA( TokenType.OP_MEMBER_ACCESS) && ConsumeOnMatchLA(TokenType.ID ) ){
                IdentifierAttributeLA();
            }else{

            }
        }
    }
}