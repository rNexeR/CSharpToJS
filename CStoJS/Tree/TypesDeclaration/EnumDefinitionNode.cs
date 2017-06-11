using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class EnumDefinitionNode : TypeDeclarationNode
    {
        
        public List<EnumNode> enum_node {get; set;}

        public EnumDefinitionNode(){
            this.type = "enum";
            this.enum_node = new List<EnumNode>();
        }

        public override void EvaluateSemantic(API api){
            Console.WriteLine($"Evaluating enum {identifier}");

            if(this.encapsulation_modifier.token != null && this.encapsulation_modifier.token.type != TokenType.PUBLIC_KEYWORD)
                throw new SemanticException("Enum cannot be defined as protected or private.", this.encapsulation_modifier.token);

            var identifiers = new List<string>();
            var previous_val = -1;
            foreach(var node in this.enum_node){
                if(identifiers.Contains(node.identifier.ToString()))
                    throw new SemanticException($"Duplicated enum item found ({node.identifier.ToString()}).", node.identifier.identifiers[0]);
                node.SetAssignment(previous_val);
                node.EvaluateSemantic();
                identifiers.Add(node.identifier.ToString());
                previous_val = node.assignment;
            }
        }
    }
}