using System;
using System.Collections.Generic;
using System.Linq;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class NamespaceNode
    {
        public IdentifierNode identifier;
        public List<UsingNode> using_array;
        public List<NamespaceNode> namespace_array;
        public List<TypeDeclarationNode> types_declaration_array;
        public int parent_position;

        public NamespaceNode()
        {
            this.parent_position = -1;
            this.using_array = new List<UsingNode>();
            this.namespace_array = new List<NamespaceNode>();
            this.types_declaration_array = new List<TypeDeclarationNode>();
            this.identifier = new IdentifierNode();
        }

        public override string ToString()
        {
            return identifier.ToString();
        }

        public void EvaluateSemantic(API api)
        {
            this.CheckUsings(api);
            this.EvaluateTypeDeclarations(api);
            this.EvaluateNamespaces(api);
        }

        private void EvaluateTypeDeclarations(API api)
        {
            foreach (var type in types_declaration_array)
            {
                type.EvaluateSemantic(api);
            }
        }

        private void EvaluateNamespaces(API api)
        {
            foreach (var nsp in namespace_array)
            {
                nsp.EvaluateSemantic(api);
            }
        }

        private void CheckUsings(API api)
        {
            var decl_names = new List<string>();
            foreach (var _using in this.using_array)
            {
                if (!api.namespaces_hash.ContainsKey(_using.identifier.ToString()))
                    throw new SemanticException($"Namespace {_using.identifier.ToString()} not found.", _using.identifier.identifiers[0]);
                
                if(_using.identifier == this.identifier)
                    continue;

                var nsp_idx = api.namespaces_hash[_using.identifier.ToString()];
                var using_decls = api.namespaces[nsp_idx].types_declaration_array;

                foreach (var type in using_decls)
                {
                    if (decl_names.Contains(type.identifier.ToString()))
                        throw new SemanticException($"Double definition of type {type.identifier.ToString()}.", _using.identifier.identifiers[0]);
                    decl_names.Add(type.identifier.ToString());
                }
            }
        }
    }
}