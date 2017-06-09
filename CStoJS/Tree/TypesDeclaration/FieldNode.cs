using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class FieldNode
    {
        public TypeDeclarationNode type;
        public IdentifierNode identifier;
        public EncapsulationNode encapsulation;
        public Token modifier; 
        public VariableInitializer assignment;

        public FieldNode(){
            
        }

        public FieldNode(TypeDeclarationNode type, IdentifierNode identifier, EncapsulationNode encapsulation, Token modifier){
            this.type = type;
            this.identifier = identifier;
            this.encapsulation = encapsulation;
            this.modifier = modifier;
        }

        public FieldNode(TypeDeclarationNode type, IdentifierNode identifier, EncapsulationNode encapsulation, Token modifier, VariableInitializer assignment) : this(type, identifier, encapsulation, modifier){
            this.assignment = assignment;
        }

        public override string ToString(){
            return this.identifier.ToString();
        }
    }
}