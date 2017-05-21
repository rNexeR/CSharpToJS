using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries{
	public partial class Parser{
		void EnumDeclaration(){
			printDebug("Enum Declaration");
			MatchExactly( new TokenType[]{ TokenType.ENUM_KEYWORD, TokenType.ID} );
			EnumBody();
			OptionalBodyEnd();
		}

		void EnumBody(){
			printDebug("Enum Body");
			MatchExactly( new TokenType[]{ TokenType.BRACE_OPEN } );
			OptionalAssignableIdentifiersList();
			MatchExactly( new TokenType[]{ TokenType.BRACE_CLOSE } );
		}

	}
	
}