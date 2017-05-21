using System.Linq;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries{
    public partial class Parser{
        void TypeDeclarationList(){
            printDebug("Type Declaration List");
            if( MatchAny( this.class_modifiers.Concat(this.encapsulation_modifiers).Concat(new TokenType[]{TokenType.CLASS_KEYWORD, TokenType.ENUM_KEYWORD, TokenType.INTERFACE_KEYWORD }).ToArray()) ){ 
                TypeDeclaration();
                TypeDeclarationList();
            }else{
                //EPSILON
            }
        }

        void TypeDeclaration(){
            printDebug("Type Declaration");
            if( MatchAny(this.encapsulation_modifiers) ){
                this.currentToken = lexer.GetNextToken();
            }
            GroupDeclaration();
        }   

        void GroupDeclaration(){
            printDebug("Group Declaration");
            if( MatchAny( this.class_modifiers.Concat(new TokenType[]{ TokenType.CLASS_KEYWORD }).ToArray() ) ){ 
                ClassDeclaration();
            }else if( MatchAny( new TokenType[]{ TokenType.ENUM_KEYWORD } ) ){
                EnumDeclaration();
            }else if( MatchAny( new TokenType[]{ TokenType.INTERFACE_KEYWORD } ) ){
                InterfaceDeclaration();
            }else{
                ThrowSyntaxException("Class, Enum or Interface Declaration expected");
            }
        }
    }
}