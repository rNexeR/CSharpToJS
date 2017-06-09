using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries
{
	public partial class Parser
	{
		public Token ClassModifier(){
			 printDebug("Class Modifier");
			if( MatchAny( this.class_modifiers ) ){
				return MatchOne(this.class_modifiers, "");
			}else{
				//EPSILON
				return null;
			}
		}

		public Token EncapsulationModifier(){
			printDebug("Encapsulation Modifier");
			if( MatchAny( this.encapsulation_modifiers ) ){
				return MatchOne(this.encapsulation_modifiers, "");
			}else{
				//EPSILON
				return null;
			}
		}

		public Token OptionalModifier(){
			printDebug("Optional Modifier");
			if( MatchAny(this.optional_modifiers) ){
				return MatchOne(this.optional_modifiers, "");
			}else{
				//EPSILON
				return null;
			}
		}
	}
}
