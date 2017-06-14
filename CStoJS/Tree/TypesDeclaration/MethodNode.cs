using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class MethodNode
    {
        public IdentifierNode identifier;
        public TypeDeclarationNode returnType;
        public EncapsulationNode encapsulation;
        public Token modifier;
        public List<ParameterNode> parameters;
        public BlockStatementNode body;

        public MethodNode()
        {
            this.parameters = new List<ParameterNode>();
            this.encapsulation = new EncapsulationNode(new Token(TokenType.PRIVATE_KEYWORD, "private", 0, 0));
        }

        public MethodNode(IdentifierNode identifier, TypeDeclarationNode returnType, EncapsulationNode encapsulation, Token modifier) : this()
        {
            this.identifier = identifier;
            this.returnType = returnType;
            this.encapsulation = encapsulation;
            this.modifier = modifier;
        }


        public override string ToString()
        {
            string ret = "";
            ret += $"{identifier.ToString()}(";
            var parameters = new List<string>();
            foreach (var x in this.parameters)
            {
                parameters.Add($"{x.type.ToString()}");
            }
            ret += string.Join(",", parameters) + ")";
            return ret;
        }

        public void EvaluateSemantic(API api, ContextManager context_manager, TypeDeclarationNode returnType)
        {
            var _usings = context_manager.GetCurrentNamespaceUsings();
            var class_name = context_manager.GetCurrentClass();

            if (modifier != null && modifier.lexema == "abstract")
            {
                if (!(api.GetTypeDeclaration(class_name) as ClassNode).isAbstract)
                    throw new SemanticException("Method cannot be abstract, class is not abstract.", modifier);
            }
            else if (modifier != null && modifier.lexema == "override")
            {
                if (!context_manager.MethodExists($"base.{this.ToString()}"))
                    throw new SemanticException("Method cannot be override, parent doesn't implement the method.", modifier);
                var method_def = context_manager.GetMethod($"base.{this.ToString()}");
                if (method_def.modifier.lexema != "override" && method_def.modifier.lexema != "virtual" && method_def.modifier.lexema != "abstract")
                {
                    throw new SemanticException("Method cannot be virtual because is not marked as virtual, override or abstract in parent.", modifier);
                }
            }

            context_manager.Push(new Context(ContextType.METHOD_CONTEXT));
            foreach (var param in this.parameters)
            {
                var param_type = param.type;
                var param_type_name = Utils.GetClassName(param_type.ToString(), _usings, api);
                if (param_type_name == "" || !api.TypeDeclarationExists(param_type_name))
                    throw new SemanticException($"Type not found {param_type.ToString()}", param.identifier.identifiers[0]);
                context_manager.AddVariableToCurrentContext(param.identifier.ToString(), api.GetTypeDeclaration(param_type_name));
            }
            this.body.Evaluate(api, context_manager, returnType);
            context_manager.Pop();
        }
    }
}