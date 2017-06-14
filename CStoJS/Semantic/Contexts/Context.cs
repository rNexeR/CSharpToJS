using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Tree;

namespace CStoJS.Semantic
{

    public class Context
    {
        public string name;
        public ContextType type;
        public Dictionary<string, TypeDeclarationNode> variables;
        public Dictionary<string, TypeDeclarationNode> methods;
        public List<string> constructors;

        public Context()
        {
            this.variables = new Dictionary<string, TypeDeclarationNode>();
            this.methods = new Dictionary<string, TypeDeclarationNode>();
            this.constructors = new List<string>();
        }

        public Context(ContextType type, string name = "") : this()
        {
            this.type = type;
            this.name = name;
        }

        public void AddVariable(string var_name, TypeDeclarationNode type, bool AddBase = false)
        {
            if (VariableExists(var_name))
                throw new SemanticException($"Double declaration of variable. Variable Name: {var_name}", type.identifier.identifiers[0]);
            variables[var_name] = type;
            if (this.type == ContextType.CLASS_CONTEXT)
            {
                variables[$"this.{var_name}"] = type;
                if (AddBase)
                    variables[$"base.{var_name}"] = type;
            }
        }

        public bool VariableExists(string var_name)
        {
            return this.variables.ContainsKey(var_name);
        }

        public TypeDeclarationNode GetVariableType(string var_name)
        {
            if (!this.variables.ContainsKey(var_name))
                return null;
            return this.variables[var_name];
        }

        

        public void AddMethod(string method_name, TypeDeclarationNode return_type, bool AddBase = false)
        {
            if (VariableExists(method_name))
                throw new SemanticException($"Double declaration of method. Method Name: {method_name}", return_type.identifier.identifiers[0]);
            methods[method_name] = return_type;
            if (this.type == ContextType.CLASS_CONTEXT || this.type == ContextType.PARENT_CLASS_CONTEXT)
            {
                methods[$"this.{method_name}"] = return_type;
                if (AddBase)
                    methods[$"base.{method_name}"] = return_type;
            }
        }

        public bool MethodExists(string method_name)
        {
            return this.methods.ContainsKey(method_name);
        }

        public TypeDeclarationNode GetMethodReturnType(string method_name)
        {
            if (!this.methods.ContainsKey(method_name))
                return null;
            return this.methods[method_name];
        }

        public void AddConstructor(string constructor_name, bool AddBase = false)
        {
            if (this.constructors.Contains(constructor_name))
                throw new SemanticException($"Double declaration of constructor with the same parameters. Constructor Name: {constructor_name}");
            constructors.Add(constructor_name);
            constructors.Add($"this{constructor_name}");
            if (AddBase)
                constructors.Add($"base{constructor_name}");
        }

        public bool ConstructorExists(string constructor_name)
        {
            return this.constructors.Contains(constructor_name);
        }

        public override string ToString()
        {
            return $"{this.type} {this.name}";
        }

    }
}