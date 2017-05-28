using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Linq;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries{
	public partial class Parser{
		ClassNode ClassDeclaration(){
			printDebug("Class Declaration");
			
			var ret = new ClassNode();

			ret.isAbstract = ClassModifier() != null;
			var tokens = MatchExactly( new TokenType[]{ TokenType.CLASS_KEYWORD, TokenType.ID } );
			ret.identifier = new IdentifierNode(tokens[1]);
			
			var inherit = InheritanceBase();
			ret.inherit = inherit;
			
			ClassBody(ref ret);
			OptionalBodyEnd();

			return ret;
		}

        private void ClassBody(ref ClassNode clase)
        {
			printDebug("Class Body");
            MatchExactly( new TokenType[]{ TokenType.BRACE_OPEN } );
			OptionalClassMemberDeclarationList(ref clase);
			MatchExactly( new TokenType[]{ TokenType.BRACE_CLOSE } );
        }

        private void OptionalClassMemberDeclarationList(ref ClassNode clase)
        {
			printDebug("Optional Class Member Declaration List");
            if( MatchAny( this.encapsulation_modifiers.Concat(optional_modifiers).Concat(this.types).Concat(new TokenType[]{TokenType.VOID_KEYWORD}).ToArray() ) ){
				ClassMemberDeclaration(ref clase);
				OptionalClassMemberDeclarationList(ref clase);
			}else{
				//EPSILON
			}
        }

        private void ClassMemberDeclaration(ref ClassNode clase)
        {
			printDebug("Class Member Declaration");

			var encap = EncapsulationModifier();
			var modifier = OptionalModifier();

			if( !Match(TokenType.ID) ){
				var type = MatchOne(this.types.Concat(new TokenType[]{TokenType.VOID_KEYWORD}).ToArray(), "");
				FieldOrMethodFactorized(encap, modifier, type, ref clase);
			}else{
				//It's ID
				var type = MatchExactly(TokenType.ID);
				if(Match(TokenType.PAREN_OPEN)){
					ConstructorDeclaration(encap, modifier, type, ref clase);
				}else{
					FieldOrMethodFactorized(encap, modifier, type, ref clase);
				}
			}
        }
    }
	
}