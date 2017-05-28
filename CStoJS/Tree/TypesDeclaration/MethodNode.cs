using System.Collections.Generic;
using CStoJS.LexerLibraries;

namespace CStoJS.Tree
{
    public class MethodNode
    {
        public IdentifierNode identifier;
        public TypeDeclarationNode  returnType;
        public EncapsulationNode encapsulation;
        public Token modifier;
        public List<ParameterNode> parameters;
        public List<StatementTypeNode> body;

        public MethodNode(){
            this.parameters = new List<ParameterNode>();
            this.body = new List<StatementTypeNode>();
        }

        public MethodNode(IdentifierNode identifier, TypeDeclarationNode returnType, EncapsulationNode encapsulation, Token modifier) : this(){
            this.identifier = identifier;
            this.returnType = returnType;
            this.encapsulation = encapsulation;
            this.modifier = modifier;
        }


        public override string ToString(){
            string ret = "";
            ret += $"{returnType.identifier} {identifier}(";
            foreach(var x in parameters){
                ret += $" {x.type} {x.identifier}, ";
            }
            return ret;
        }
    }
}