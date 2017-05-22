using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;

namespace CStoJS.ParserLibraries{
	public partial class Parser
    {
        public void InheritanceBase(){
            printDebug("Inheritance Base");
            if( Match(TokenType.OP_HIERARCHY) ){
                ConsumeToken();
                IdentifierList();
            }else{
                //EPSILON
            }
        }    
    }
}