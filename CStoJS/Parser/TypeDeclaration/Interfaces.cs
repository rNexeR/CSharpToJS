using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;
using CStoJS.Tree;
using System.Collections.Generic;

namespace CStoJS.ParserLibraries{
	public partial class Parser{
		InterfaceNode InterfaceDeclaration(){
			printDebug("Interface Declaration");
			
			var token = MatchExactly( new TokenType[]{ TokenType.INTERFACE_KEYWORD, TokenType.ID } );
			var identifier = new IdentifierNode(token[1]);

			var inherit = InheritanceBase();
			var methods = InterfaceBody();
			OptionalBodyEnd();
			return new InterfaceNode(identifier, inherit, methods);
		}

		List<MethodNode> InterfaceBody(){
			printDebug("Interface Body");
			MatchExactly( new TokenType[]{ TokenType.BRACE_OPEN } );
			var methods = InterfaceMethodDeclarationList();
			MatchExactly( new TokenType[]{ TokenType.BRACE_CLOSE } );
			return methods;
		}

		List<MethodNode> InterfaceMethodDeclarationList(){
			printDebug("Interface Method Declaration List");
			if(MatchAny( this.types.Concat( new TokenType[]{ TokenType.VOID_KEYWORD } ).ToArray() ) ){

				var method = InterfaceMethodHeader();
				MatchExactly(new TokenType[]{ TokenType.END_STATEMENT });
				var methods = InterfaceMethodDeclarationList();
				methods.Insert(0, method);
				return methods;
			}else{
				return new List<MethodNode>();
			}
		}

		MethodNode InterfaceMethodHeader(){
			printDebug("Interface Method Header");

			var ret = new MethodNode();
			var type = TypeOrVoid();
			ret.returnType = type;

			var tokens = MatchExactly( new TokenType[]{ TokenType.ID, TokenType.PAREN_OPEN } );

			ret.identifier = new IdentifierNode(tokens[0]);

			ret.parameters = FixedParameters();

			MatchExactly( new TokenType[]{ TokenType.PAREN_CLOSE } );
			return ret;
		}

        
    }
}