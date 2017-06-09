using System;
using CStoJS.Exceptions;

namespace CStoJS.Tree
{
    public class EnumNode
    {
        public IdentifierNode identifier;
        public int assignment;

        public EnumNode(){
            this.assignment = -1;
        }

        public void EvaluateSemantic()
        {
            if(this.assignment < 0)
                throw new SemanticException("Enum Item assignation cannot be < 0", identifier.identifiers[0]);
        }

        public void SetAssignment(int previous_val)
        {
             if(this.assignment < 0)
                this.assignment = previous_val+1;
        }
    }
}