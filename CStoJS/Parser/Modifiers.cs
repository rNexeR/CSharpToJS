using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries
{
	public partial class Parser
	{
		public void ClassModifier(){
			 printDebug("Class Modifier");
			if( MatchAny( this.class_modifiers ) ){
				ConsumeToken();
			}else{
				//EPSILON
			}
		}

		public void EncapsulationModifier(){
			printDebug("Encapsulation Modifier");
			if( MatchAny( this.encapsulation_modifiers ) ){
				ConsumeToken();
			}else{
				//EPSILON
			}
		}

		public void OptionalModifier(){
			printDebug("Optional Modifier");
			if( MatchAny(this.optional_modifiers) ){
				MatchOne(this.optional_modifiers, "");
			}else{
				//EPSILON
			}
		}
	}
}
