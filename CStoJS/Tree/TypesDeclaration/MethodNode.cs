using System;
using System.Collections.Generic;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class MethodNode
    {
        public IdentifierNode identifier;
        public TypeDeclarationNode  returnType;
        public EncapsulationNode encapsulation;
        public Token modifier;
        public List<ParameterNode> parameters;
        public BlockStatementNode body;

        public MethodNode(){
            this.parameters = new List<ParameterNode>();
            this.encapsulation = new EncapsulationNode(new Token(TokenType.PRIVATE_KEYWORD, "private", 0, 0));
        }

        public MethodNode(IdentifierNode identifier, TypeDeclarationNode returnType, EncapsulationNode encapsulation, Token modifier) : this(){
            this.identifier = identifier;
            this.returnType = returnType;
            this.encapsulation = encapsulation;
            this.modifier = modifier;
        }


        public override string ToString(){
            string ret = "";
            ret += $"{identifier.ToString()}(";
            var parameters = new List<string>();
            foreach(var x in this.parameters){
                parameters.Add($"{x.type.ToString()}");
            }
            ret += string.Join(",", parameters) + ")";
            return ret;
        }

        public void EvaluateSemantic(API api, ContextManager context_manager)
        {
            // throw new NotImplementedException();
        }
    }
}