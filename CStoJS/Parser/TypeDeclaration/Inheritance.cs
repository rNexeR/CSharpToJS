using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using System;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries{
	public partial class Parser
    {
        public List<IdentifierNode> InheritanceBase(){
            printDebug("Inheritance Base");
            if( Match(TokenType.OP_HIERARCHY) ){
                ConsumeToken();
                return IdentifierList();
            }else{
                return new List<IdentifierNode>();
            }
        }    
    }
}