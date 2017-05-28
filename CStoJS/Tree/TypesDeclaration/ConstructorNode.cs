using System.Collections.Generic;
using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class ConstructorNode
    {
        public IdentifierNode identifier;
        public TypeDeclarationNode  returnType;
        public EncapsulationNode encapsulation;
        public Token modifier;
        public List<ParameterNode> parameters;
        public List<StatementTypeNode> body;
        public List<ArgumentNode> initializer;
        
        public ConstructorNode()
        {
            this.parameters = new List<ParameterNode>();
            this.body = new List<StatementTypeNode>();
        }
        public ConstructorNode(IdentifierNode identifier, EncapsulationNode encapsulation, Token modifier)
        {
            this.identifier = identifier;
            this.encapsulation = encapsulation;
            this.modifier = modifier;
        }
    }
}