using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Tree;

namespace CStoJS.Semantic
{
    public class ContextManager
    {
        public readonly API api;
        public List<Context> contexts;

        public ContextManager()
        {
            this.contexts = new List<Context>();
        }

        public ContextManager(API api) : this()
        {
            this.api = api;
        }

        public void Push(Context context, string class_name = "")
        {
            this.contexts.Add(context);
            if (context.type == ContextType.CLASS_CONTEXT)
            {
                this.AddClassMembers(class_name);
                this.PushParentClasses(class_name);
            }
        }

        private void AddClassMembers(string class_name, bool last_context = true, bool AddBase = false)
        {
            if (!this.api.TypeDeclarationExists(class_name))
            {
                Console.WriteLine($"Class {class_name} not found in api.");
                return;
            }

            var clase = this.api.GetTypeDeclaration(class_name) as ClassNode;

            foreach (var field in clase.fields)
            {
                this.AddVariableToCurrentContext(field.identifier.ToString(), field.type, AddBase);
            }

            foreach (var method in clase.methods)
            {
                this.AddVariableToCurrentContext(method.identifier.ToString(), method.returnType, AddBase);
            }

            foreach (var ctor in clase.constructors)
            {
                this.AddConstructorToCurrentContext(ctor.ToString(), AddBase);
            }
        }

        private void PushParentClasses(string class_name)
        {
            if (!this.api.TypeDeclarationExists(class_name))
            {
                Console.WriteLine($"Class {class_name} not found in api.");
                return;
            }
            if (class_name == "System.Object")
                return;
            var clase = this.api.GetTypeDeclaration(class_name) as ClassNode;
            var parent_class_name_identifiers = clase.inherit;
            var parent_class_name = "System.Object";

            var identifiers = new List<string>();
            foreach (var identifier in parent_class_name_identifiers)
            {
                identifiers.Add(identifier.ToString());
            }

            if (identifiers.Count > 0)
            {
                parent_class_name = string.Join(".", identifiers);
            }

            bool found = this.api.TypeDeclarationExists(parent_class_name);

            if (!found)
            {

                var nsp_usings = this.api.namespaces[clase.namespace_index].using_array;

                foreach (var _using in nsp_usings)
                {
                    var nsp_name = _using.identifier.ToString();
                    if (this.api.TypeDeclarationExists($"{nsp_name}.{parent_class_name}"))
                    {
                        parent_class_name = $"{nsp_name}.{parent_class_name}";
                        found = true;
                    }
                }
            }

            if (!found)
                throw new SemanticException("Base class not found.", clase.inherit[0].identifiers[0]);
            
            this.contexts.Insert(0, new Context(ContextType.CLASS_CONTEXT, parent_class_name));
            this.AddClassMembers(parent_class_name, false, true);
            this.PushParentClasses(parent_class_name);
        }

        public void Pop()
        {
            this.contexts.RemoveAt(this.contexts.Count - 1);
        }

        public void Clear()
        {
            this.contexts = new List<Context>();
        }

        public void AddVariableToCurrentContext(string var_name, TypeDeclarationNode type, bool AddBase = false)
        {
            if (this.contexts.Count == 0)
            {
                Console.WriteLine("No contexts added");
                return;
            }
            this.contexts[this.contexts.Count - 1].AddVariable(var_name, type, AddBase);
        }

        public bool VariableExists(string var_name)
        {
            for (int i = this.contexts.Count - 1; i >= 0; i--)
            {
                if (contexts[i].VariableExists(var_name))
                    return true;
            }
            return false;
        }

        public TypeDeclarationNode GetVariableType(string var_name)
        {
            for (int i = this.contexts.Count - 1; i >= 0; i--)
            {
                if (contexts[i].VariableExists(var_name))
                    return contexts[i].GetVariableType(var_name);
            }
            return null;
        }

        public void AddMethodToCurentContext(string name, TypeDeclarationNode return_type, bool AddBase = false)
        {
            if (this.contexts.Count == 0)
            {
                Console.WriteLine("No contexts added");
                return;
            }

            var expected_contexts_types = new List<ContextType> { ContextType.CLASS_CONTEXT/*, ContextType.INTERFACE_CONTEXT*/};
            if (!expected_contexts_types.Contains(this.contexts[this.contexts.Count - 1].type))
                throw new SemanticException("Method only can be added to Class or Interface Context", return_type.identifier.identifiers[0]);

            this.contexts[this.contexts.Count - 1].AddMethod(name, return_type, AddBase);
        }

        public bool MethodExists(string name)
        {
            for (int i = this.contexts.Count - 1; i >= 0; i--)
            {
                if (contexts[i].MethodExists(name))
                    return true;
            }
            return false;
        }

        public TypeDeclarationNode GetMethodReturnType(string name)
        {
            for (int i = this.contexts.Count - 1; i >= 0; i--)
            {
                if (contexts[i].MethodExists(name))
                    return contexts[i].GetMethodReturnType(name);
            }
            return null;
        }

        public void AddConstructorToCurrentContext(string name, bool AddBase = false)
        {
            if (this.contexts.Count == 0)
            {
                Console.WriteLine("No contexts added");
                return;
            }

            var expected_contexts_types = new List<ContextType> { ContextType.CLASS_CONTEXT/*, ContextType.INTERFACE_CONTEXT*/};
            if (!expected_contexts_types.Contains(this.contexts[this.contexts.Count - 1].type))
                throw new SemanticException("Method only can be added to Class or Interface Context");

            this.contexts[this.contexts.Count - 1].AddConstructor(name, AddBase);
        }

        public bool ConstructorExists(string name)
        {
            for (int i = this.contexts.Count - 1; i >= 0; i--)
            {
                if (contexts[i].ConstructorExists(name))
                    return true;
            }
            return false;
        }
    }
}