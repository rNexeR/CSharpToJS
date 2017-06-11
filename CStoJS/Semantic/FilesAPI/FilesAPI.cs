using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Tree;

namespace CStoJS.Semantic
{
    public class API
    {
        public readonly List<NamespaceNode> namespaces;
        public Dictionary<string, int> namespaces_hash;
        public Dictionary<string, TypeDeclarationNode> types;

        public API(List<NamespaceNode> trees)
        {
            this.namespaces = new List<NamespaceNode>();
            GenerateTypesHash(trees);
        }

        public bool TypeDeclarationExists(string typename)
        {
            return this.types.ContainsKey(typename);
        }

        public TypeDeclarationNode GetTypeDeclaration(string typename)
        {
            if (!TypeDeclarationExists(typename))
                return null;
            return this.types[typename];
        }

        public Dictionary<string, MethodNode> GetClassMethods(ClassNode clase)
        {
            var ret = new Dictionary<string, MethodNode>();
            foreach (var method in clase.methods)
            {
                var method_name = method.identifier.ToString();
                var parameters = new List<string>();
                foreach (var parameter in method.parameters)
                {
                    parameters.Add(parameter.type.ToString());
                }
                method_name += "(" + string.Join(",", parameters) + ")";
                ret[method_name] = method;
            }
            return ret;
        }

        public bool ClassMethodExists(ClassNode clase, string method_name)
        {
            return GetClassMethods(clase).ContainsKey(method_name);
        }

        public MethodNode GetClassMethod(ClassNode clase, string method_name)
        {
            var methods = GetClassMethods(clase);
            if (!methods.ContainsKey(method_name))
                return null;
            return methods[method_name];
        }

        public Dictionary<string, MethodNode> GetInterfaceMethods(InterfaceNode _interface)
        {
            var ret = new Dictionary<string, MethodNode>();
            foreach (var method in _interface.methods)
            {
                var method_name = method.identifier.ToString();
                var parameters = new List<string>();
                foreach (var parameter in method.parameters)
                {
                    parameters.Add(parameter.type.ToString());
                }
                method_name += "(" + string.Join(",", parameters) + ")";
                ret[method_name] = method;
            }
            return ret;
        }

        public bool InterfaceMethodExists(InterfaceNode _interface, string method_name)
        {
            return GetInterfaceMethods(_interface).ContainsKey(method_name);
        }

        public MethodNode GetInterfaceMethod(InterfaceNode _interface, string method_name)
        {
            var methods = GetInterfaceMethods(_interface);
            if (!methods.ContainsKey(method_name))
                return null;
            return methods[method_name];
        }

        public Dictionary<string, ConstructorNode> GetClassConstructors(ClassNode clase)
        {
            var ret = new Dictionary<string, ConstructorNode>();

            foreach (var ctor in clase.constructors)
            {
                var method_name = ctor.identifier.ToString();
                var parameters = new List<string>();
                foreach (var parameter in ctor.parameters)
                {
                    parameters.Add(parameter.type.ToString());
                }
                method_name += "(" + string.Join(",", parameters) + ")";
                ret[method_name] = ctor;
            }

            return ret;
        }

        public bool ClassConstructorExists(ClassNode clase, string ctor_name)
        {
            return GetClassConstructors(clase).ContainsKey(ctor_name);
        }

        public ConstructorNode GetClassConstructor(ClassNode clase, string ctor_name)
        {
            var ctors = GetClassConstructors(clase);
            if (!ctors.ContainsKey(ctor_name))
                return null;
            return ctors[ctor_name];
        }

        public Dictionary<string, FieldNode> GetClassFields(ClassNode clase)
        {
            var ret = new Dictionary<string, FieldNode>();

            foreach (var field in clase.fields)
            {
                var field_name = field.identifier.ToString();
                ret[field_name] = field;
            }

            return ret;
        }

        public bool ClassFieldExists(ClassNode clase, string field_name)
        {
            return GetClassFields(clase).ContainsKey(field_name);
        }

        public FieldNode GetClassField(ClassNode clase, string field_name)
        {
            var fields = GetClassFields(clase);
            if (!fields.ContainsKey(field_name))
                return null;
            return fields[field_name];
        }

        private void GenerateTypesHash(List<NamespaceNode> trees)
        {
            this.types = new Dictionary<string, TypeDeclarationNode>();
            foreach (var tree in trees)
            {
                namespaces.AddRange(GetNamespaces(tree));
            }

            this.namespaces_hash = new Dictionary<string, int>();
            var nsp_idx = 0;
            foreach (var nsp in namespaces)
            {
                namespaces_hash[nsp.ToString()] = nsp_idx;

                foreach (var dcl in nsp.types_declaration_array)
                {
                    string typename = dcl.identifier.ToString();
                    if (nsp.ToString() != "")
                        typename = $"{nsp.identifier.ToString()}.{dcl.identifier.ToString()}";
                    dcl.namespace_index = nsp_idx;
                    if (types.ContainsKey(typename))
                        throw new SemanticException($"Double definition of Type {typename}.", dcl.identifier.identifiers[0]);
                    types[typename] = dcl;
                }
                nsp_idx++;
            }
        }

        private List<NamespaceNode> GetNamespaces(NamespaceNode tree)
        {
            var ret = new List<NamespaceNode>();
            GetNamespaces(ref ret, tree, new IdentifierNode());

            return ret;
        }
        private void GetNamespaces(ref List<NamespaceNode> lista, NamespaceNode tree, IdentifierNode parent_identifiers)
        {

            var current_namespace = new NamespaceNode();
            current_namespace.identifier = tree.identifier;
            current_namespace.using_array = tree.using_array;
            current_namespace.types_declaration_array = tree.types_declaration_array;

            int parent_pos = -1;
            var parent_name = parent_identifiers.ToString();
            var i = 0;
            foreach (var nsp in lista)
            {
                if (nsp.ToString() == parent_name)
                    parent_pos = i;
                i++;
            }

            current_namespace.parent_position = parent_pos;
            if (parent_pos >= 0)
            {
                current_namespace.using_array.AddRange(lista[parent_pos].using_array);
                // current_namespace.using_array.Add(new UsingNode(lista[parent_pos].identifier));
            }
            current_namespace.using_array.Add(new UsingNode(current_namespace.identifier));
            current_namespace.identifier.identifiers.InsertRange(0, parent_identifiers.identifiers);

            lista.Add(current_namespace);


            foreach (var nspname in tree.namespace_array)
            {
                GetNamespaces(ref lista, nspname, current_namespace.identifier);
            }
        }

        private List<NamespaceNode> JoinNamespaces(List<List<NamespaceNode>> nsp_array)
        {
            var ret = new List<NamespaceNode>();

            foreach (var nsp_list in nsp_array)
            {
                ret.AddRange(nsp_list);
            }

            return ret;
        }
    }
}