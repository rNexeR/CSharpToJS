using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
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
                if (class_name != "Object")
                {
                    this.PushParentClasses(class_name);
                    this.contexts.Insert(0, new Context(ContextType.PARENT_CLASS_CONTEXT, "Object"));
                    this.AddClassMembers("Object", true, false, 0);
                }

            }
        }

        public List<UsingNode> GetCurrentNamespaceUsings()
        {
            foreach (var context in this.contexts)
            {
                if (context.type == ContextType.CLASS_CONTEXT)
                {
                    var class_name = context.name;
                    var clase = this.api.GetTypeDeclaration(class_name) as ClassNode;
                    return this.api.namespaces[clase.namespace_index].using_array;
                }

            }
            return new List<UsingNode>();
        }

        public string GetCurrentClass(){
            foreach (var context in this.contexts)
            {
                if (context.type == ContextType.CLASS_CONTEXT)
                {
                    return context.name;
                }
            }
            return "";
        }

        public List<Context> getParentsContexts()
        {
            var ret = new List<Context>();
            foreach (var ctx in this.contexts)
            {
                if (ctx.type != ContextType.PARENT_CLASS_CONTEXT && ctx.type != ContextType.PARENT_INTERFACE_CONTEXT)
                    break;
                ret.Add(ctx);
            }
            return ret;
        }

        private void AddClassMembers(string class_name, bool AddBase = false, bool CurrentContext = true, int ctx_id = 0)
        {
            var ctx = CurrentContext ? this.contexts.Count - 1 : ctx_id;
            if (!this.api.TypeDeclarationExists(class_name))
            {
                Console.WriteLine($"Class {class_name} not found in api.");
                return;
            }

            var clase = this.api.GetTypeDeclaration(class_name) as TypeDefinitionNode;

            foreach (var field in clase.fields)
            {
                if (AddBase && field.encapsulation.token.lexema == "private")
                    continue;
                this.AddVariableToContext(ctx, field.ToString(), field.type, AddBase);
            }

            foreach (var method in clase.methods)
            {
                if (AddBase && method.encapsulation.token.lexema == "private")
                    continue;
                this.AddMethodToContext(ctx, method.ToString(), method.returnType, AddBase);
            }

            foreach (var ctor in clase.constructors)
            {
                if (AddBase && ctor.encapsulation.token.lexema == "private")
                    continue;
                this.AddConstructorToContext(ctx, ctor.ToString(), AddBase);
            }
        }

        private void AddClassMembersToContext(int ctx_id, string class_name, bool AddBase = false)
        {
            if (!this.api.TypeDeclarationExists(class_name))
            {
                Console.WriteLine($"Class {class_name} not found in api.");
                return;
            }

            var clase = this.api.GetTypeDeclaration(class_name) as ClassNode;

            foreach (var field in clase.fields)
            {
                this.AddVariableToContext(ctx_id, field.identifier.ToString(), field.type, AddBase);
            }

            foreach (var method in clase.methods)
            {
                this.AddMethodToContext(ctx_id, method.identifier.ToString(), method.returnType, AddBase);
            }

            foreach (var ctor in clase.constructors)
            {
                this.AddConstructorToContext(ctx_id, ctor.ToString(), AddBase);
            }
        }

        private void AddVariableToContext(int ctx_id, string var_name, TypeDeclarationNode type, bool AddBase = false)
        {
            if (this.contexts.Count == 0 || this.contexts.Count < ctx_id)
            {
                Console.WriteLine("Context Not Found");
                return;
            }
            this.contexts[ctx_id].AddVariable(var_name, type, AddBase);
        }

        public void AddMethodToContext(int ctx_id, string name, TypeDeclarationNode return_type, bool AddBase = false)
        {
            if (this.contexts.Count == 0 || this.contexts.Count < ctx_id)
            {
                Console.WriteLine("Context Not Found");
                return;
            }

            var expected_contexts_types = new List<ContextType> { ContextType.CLASS_CONTEXT, ContextType.PARENT_CLASS_CONTEXT/*, ContextType.INTERFACE_CONTEXT*/};
            if (!expected_contexts_types.Contains(this.contexts[this.contexts.Count - 1].type))
                throw new SemanticException("Method only can be added to Class or Interface Context", return_type.identifier.identifiers[0]);

            this.contexts[ctx_id].AddMethod(name, return_type, AddBase);
        }

        public void AddConstructorToContext(int ctx_id, string name, bool AddBase = false)
        {
            if (this.contexts.Count == 0 || this.contexts.Count < ctx_id)
            {
                Console.WriteLine("Context Not Found");
                return;
            }

            var expected_contexts_types = new List<ContextType> { ContextType.CLASS_CONTEXT, ContextType.PARENT_CLASS_CONTEXT/*, ContextType.INTERFACE_CONTEXT*/};
            if (!expected_contexts_types.Contains(this.contexts[this.contexts.Count - 1].type))
                throw new SemanticException("Method only can be added to Class or Interface Context");

            this.contexts[ctx_id].AddConstructor(name, AddBase);
        }

        private void PushParentClasses(string class_name)
        {
            if (!this.api.TypeDeclarationExists(class_name))
            {
                Console.WriteLine($"Class {class_name} not found in api.");
                return;
            }

            var clase = this.api.GetTypeDeclaration(class_name) as TypeDefinitionNode;
            var parent_class_name_identifiers = clase.inherit;

            foreach (var parent in parent_class_name_identifiers)
            {
                var parent_class_name = parent.ToString();
                bool found = this.api.TypeDeclarationExists(parent.ToString());

                if (!found)
                {

                    var nsp_usings = this.api.namespaces[clase.namespace_index].using_array;
                    // nsp_usings.Insert(0, new UsingNode(this.api.namespaces[clase.namespace_index].identifier));

                    foreach (var _using in nsp_usings)
                    {
                        var nsp_name = _using.identifier.ToString();
                        if (this.api.TypeDeclarationExists($"{nsp_name}.{parent.ToString()}"))
                        {
                            parent_class_name = $"{nsp_name}.{parent_class_name}";
                            found = true;
                            break;
                        }
                    }
                }

                var parent_type = ContextType.PARENT_CLASS_CONTEXT;
                if (api.GetTypeDeclaration(parent_class_name) is InterfaceNode)
                    parent_type = ContextType.PARENT_INTERFACE_CONTEXT;

                if (!found)
                    throw new SemanticException($"Base class not found, Base name: {clase.inherit[0]}", clase.inherit[0].identifiers[0]);

                foreach (var ctx in this.contexts)
                    if (ctx.name == parent_class_name)
                        throw new SemanticException($"Cycle detected in type {parent_class_name}");

                this.contexts.Insert(0, new Context(parent_type, parent_class_name));
                this.AddClassMembers(parent_class_name, true, false, 0);
                this.PushParentClasses(parent_class_name);
            }


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
            this.AddVariableToContext(this.contexts.Count - 1, var_name, type, AddBase);
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
            AddMethodToContext(this.contexts.Count - 1, name, return_type, AddBase);
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
            AddConstructorToContext(this.contexts.Count - 1, name, AddBase);
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