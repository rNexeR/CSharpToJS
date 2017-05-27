using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using CStoJS.Tree;
using System.Collections.Generic;

namespace CStoJS.ParserLibraries{
	public partial class Parser{
		TypeDeclarationNode EnumDeclaration(){
			printDebug("Enum Declaration");

			var enumNode = new EnumDefinitionNode();

			var tokens = MatchExactly( new TokenType[]{ TokenType.ENUM_KEYWORD, TokenType.ID} );
			enumNode.identifier.identifiers.Add(tokens[1]);
			enumNode.enum_node =  EnumBody();
			OptionalBodyEnd();
			
			return enumNode;
		}

		List<EnumNode> EnumBody(){
			printDebug("Enum Body");

			var lista = new List<EnumNode>();

			MatchExactly( new TokenType[]{ TokenType.BRACE_OPEN } );
			OptionalAssignableIdentifiersList(ref lista);
			MatchExactly( new TokenType[]{ TokenType.BRACE_CLOSE } );

			return lista;
		}

	}
	
}