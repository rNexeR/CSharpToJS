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
        public StatementNode body;
        public ConstructorInitializerNode initializer;
        
        public ConstructorNode()
        {
            this.parameters = new List<ParameterNode>();
            this.encapsulation = new EncapsulationNode(new Token(TokenType.PRIVATE_KEYWORD, "private", 0, 0));
        }
        public ConstructorNode(IdentifierNode identifier, EncapsulationNode encapsulation, Token modifier)
        {
            this.identifier = identifier;
            this.encapsulation = encapsulation;
            this.modifier = modifier;
        }

        public override string ToString(){
            var ret = $"(";

            var param = new List<string>();
            foreach(var parameter in parameters){
                param.Add(parameter.type.ToString());
            }

            ret += string.Join(",",param) + ")";
            
            return ret;
        }
    }
}