using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;

namespace CStoJS.ParserLibraries{
	public partial class Parser{
		void InterfaceDeclaration(){
			printDebug("Interface Declaration");
			MatchExactly( new TokenType[]{ TokenType.INTERFACE_KEYWORD, TokenType.ID } );
			InheritanceBase();
			InterfaceBody();
			OptionalBodyEnd();
		}

		void InterfaceBody(){
			printDebug("Interface Body");
			MatchExactly( new TokenType[]{ TokenType.BRACE_OPEN } );
			InterfaceMethodDeclarationList();
			MatchExactly( new TokenType[]{ TokenType.BRACE_CLOSE } );
		}

		void InterfaceMethodDeclarationList(){
			printDebug("Interface Method Declaration List");
			if(MatchAny( this.types.Concat( new TokenType[]{ TokenType.VOID_KEYWORD } ).ToArray() ) ){
				InterfaceMethodHeader();
				MatchExactly(new TokenType[]{ TokenType.END_STATEMENT });
				InterfaceMethodDeclarationList();
			}else{
				//EPSILON
			}
		}

		void InterfaceMethodHeader(){
			printDebug("Interface Method Header");
			TypeOrVoid();
			MatchExactly( new TokenType[]{ TokenType.ID, TokenType.PAREN_OPEN } );
			FixedParameters();
			MatchExactly( new TokenType[]{ TokenType.PAREN_CLOSE } );
		}

        
    }
	
}