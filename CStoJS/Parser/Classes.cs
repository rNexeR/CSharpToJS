using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;

namespace CStoJS.ParserLibraries{
	public partial class Parser{
		void ClassDeclaration(){
			printDebug("Class Declaration");
			ClassModifier();
			MatchExactly( new TokenType[]{ TokenType.CLASS_KEYWORD, TokenType.ID } );
			InheritanceBase();
			ClassBody();
			OptionalBodyEnd();
		}

        private void ClassBody()
        {
			printDebug("Class Body");
            MatchExactly( new TokenType[]{ TokenType.BRACE_OPEN } );
			OptionalClassMemberDeclarationList();
			MatchExactly( new TokenType[]{ TokenType.BRACE_CLOSE } );
        }

        private void OptionalClassMemberDeclarationList()
        {
			printDebug("Optional Class Member Declaration List");
            if( MatchAny( this.encapsulation_modifiers.Concat(optional_modifiers).Concat(this.types).Concat(new TokenType[]{TokenType.VOID_KEYWORD}).ToArray() ) ){
				ClassMemberDeclaration();
				OptionalClassMemberDeclarationList();
			}else{
				//EPSILON
			}
        }

        private void ClassMemberDeclaration()
        {
			printDebug("Class Member Declaration");
			EncapsulationModifier();
			OptionalModifier();
			if( !Match(TokenType.ID) ){
				ConsumeToken();
				FieldOrMethodFactorized();
			}else{
				//It's ID
				ConsumeToken();
				if(Match(TokenType.PAREN_OPEN)){
					ConstructorDeclaration();
				}else{
					FieldOrMethodFactorized();
				}
			}
        }
    }
	
}