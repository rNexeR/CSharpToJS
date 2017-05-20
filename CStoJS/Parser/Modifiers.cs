namespace CStoJS.Parser
{
	public partial class Parser
	{
		public void ClassModifier(){
			if( MatchAny( this.class_modifiers ) ){
				currentToken = this.lexer.GetNextToken();
			}else{
				//EPSILON
			}
		}

		public void EncapsulationModifier(){
			if( MatchAny( this.encapsulation_modifiers ) ){
				currentToken = this.lexer.GetNextToken();
			}else{
				//EPSILON
			}
		}
	}
}
