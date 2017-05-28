using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class FieldNode
    {
        public TypeDeclarationNode type;
        public IdentifierNode identifier;
        public EncapsulationNode encapsulation;
        public Token modifier; 
        public AssignmentNode assignment;

        public FieldNode(){
            
        }

        public FieldNode(TypeDeclarationNode type, IdentifierNode identifier, EncapsulationNode encapsulation, Token modifier){
            this.type = type;
            this.identifier = identifier;
            this.encapsulation = encapsulation;
            this.modifier = modifier;
        }

        public FieldNode(TypeDeclarationNode type, IdentifierNode identifier, EncapsulationNode encapsulation, Token modifier, AssignmentNode assignment) : this(type, identifier, encapsulation, modifier){
            this.assignment = assignment;
        }
    }
}