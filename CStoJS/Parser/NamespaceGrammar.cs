using CStoJS.LexerLibraries;

namespace CStoJS.Parser
{
    public partial class Parser
    {
        void CompilationUnit(){
            if( MatchAny( {TokenType.USING_KEYWORD}) ){
                OptionalUsingDirective();
            }else if( MatchAny( { TokenType.NAMESPACE_KEYWORD }) ){
                OpionalNamespaceMemberDeclaration();
            }else if( MatchAny( this.class_modifiers.Concat(this.encapsulation_modifiers).Concat({TokenType.CLASS_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.INTERFACE_KEYWORD })) ){
                TypeDeclarationList();
            }else{
                throw new SyntaxException("Using Directive, Namespace Declaration or Type Declaration Expected");
            }
        }

        void OptionalUsingDirective(){
            if( MatchAny( { TokenType.NAMESPACE_KEYWORD }) ){
                UsingDirective();
            }else{

            }
        }

        void OpionalNamespaceMemberDeclaration(){
            if( MatchAny(currentToken, { TokenType.NAMESPACE_KEYWORD }) ){
                NamespaceMemberDeclaration();
            }else{
                
            }
        }

        void UsingDirective(){
            MatchExactly({TokenType.USING_KEYWORD, TokenType.ID});
            IdentifierAttribute();
            MatchExactly( {TokenType.END_STATEMENT} );
            OptionalUsingDirective();
        }

        void NamespaceMemberDeclaration(){
             if( MatchAny( { TokenType.NAMESPACE_KEYWORD }) ){
               NamespaceDeclaration();
            }else{
                TypeDeclarationList();
            }
        }

        void NamespaceDeclaration(){
            MatchExactly( {TokenType.NAMESPACE_KEYWORD, TokenType.ID} );
            IdentifierAttribute();
            NamespaceBody();
        }

        void NamespaceBody(){
            MatchExactly( {TokenType.BRACE_OPEN} );
            OptionalUsingDirective();
            OpionalNamespaceMemberDeclaration();
            MatchExactly( {TokenType.BRACE_CLOSE} );
        }

        void IdentifierAttribute(){
            if( OptionalMatchExactly( { TokenType.OP_MEMBER_ACCESS, TokenType.ID } ))
                IdentifierAttribute();
        }
    }
}