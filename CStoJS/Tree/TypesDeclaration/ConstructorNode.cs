using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ConstructorNode
    {
        public IdentifierNode identifier;
        public TypeDeclarationNode returnType;
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

        public override string ToString()
        {
            var ret = $"(";

            var param = new List<string>();
            foreach (var parameter in parameters)
            {
                param.Add(parameter.type.ToString());
            }

            ret += string.Join(",", param) + ")";

            return ret;
        }

        internal void Evaluate(API api, ContextManager context_manager, string class_name)
        {
            var _usings = context_manager.GetCurrentNamespaceUsings();
            if (this.identifier.ToString() != class_name)
                throw new SemanticException("Construthis's name must be the same as the name of the class.", this.identifier.identifiers[0]);
            context_manager.Push(new Context(ContextType.CONSTRUCTOR_CONTEXT));
            foreach (var param in this.parameters)
            {
                var param_type = param.type;
                var param_type_name = Utils.GetClassName(param_type.ToString(), _usings, api);
                if (param_type_name == "")
                    throw new SemanticException($"Type not found {param_type.ToString()}", param.identifier.identifiers[0]);
                context_manager.AddVariableToCurrentContext(param.identifier.ToString(), param.type);
            }
            
            
            if (this.initializer != null)
            {
                this.initializer.Evaluate(api, context_manager);
            }
            this.body.Evaluate(api, context_manager, new VoidType());
            context_manager.Pop();
        }
    }
}