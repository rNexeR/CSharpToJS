using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class InterfaceNode : TypeDeclarationNode
    {
        public IdentifierNode namespace_name;
        public List<IdentifierNode> inherit;
        public List<MethodNode> methods;
        public InterfaceNode()
        {
            this.type = "interface";
            this.inherit = new List<IdentifierNode>();
            this.methods = new List<MethodNode>();
        }

        public InterfaceNode(IdentifierNode identifier, List<IdentifierNode> inherit, List<MethodNode> methods) : this()
        {
            this.inherit = inherit;
            this.methods = methods;
            this.identifier = identifier;
        }

        public void AddBase(IdentifierNode identifier)
        {
            this.inherit.Add(identifier);
        }

        public void AddMethod(MethodNode method)
        {
            this.methods.Add(method);
        }

        public override void EvaluateSemantic(API api)
        {
            Console.WriteLine($"Evaluating {identifier}");
            //TO-DO: inherits
            var methods = new List<string>();
            foreach (var base_type in inherit)
            {
                var _usings = api.namespaces[this.namespace_index].using_array;
                _usings.Insert(0, new UsingNode(api.namespaces[this.namespace_index].identifier));
                var found = false;
                foreach (var _using in _usings)
                {
                    if (api.TypeDeclarationExists($"{_using.ToString()}.{base_type.ToString()}"))
                    {
                        var type = api.GetTypeDeclaration($"{_using.ToString()}.{base_type.ToString()}");
                        if(!(type is InterfaceNode))
                            throw new SemanticException("Interface can olny inherits from interfaces.", base_type.identifiers[0]);
                        //Add base methods
                        foreach(var method in (type as InterfaceNode).methods)
                            methods.Add(method.ToString());
                        found = true;
                        break;
                    }
                }
                if (!found)
                    throw new SemanticException($"Base Interface not found. Interface name {base_type.ToString()}", base_type.identifiers[0]);
            }

            foreach (var method in this.methods)
            {
                if (methods.Contains(method.ToString()))
                    throw new SemanticException($"Double definition of Interface Method. Method Name: {method.ToString()}.", method.identifier.identifiers[0]);
                // method.EvaluateSemantic(api, context_manager);
                methods.Add(method.ToString());
            }
        }
    }
}