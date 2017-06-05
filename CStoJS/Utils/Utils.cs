using System.Collections.Generic;
using CStoJS.LexerLibraries;
using CStoJS.Tree;

namespace Utils{
	public static class SemanticUtils{

		public static List<NamespaceNode> GetNamespaces(NamespaceNode tree){
			var ret = new List<NamespaceNode>();
			GetNamespaces(ref ret, tree, new IdentifierNode());
			return ret;
		}
		private static void GetNamespaces(ref List<NamespaceNode> lista, NamespaceNode tree, IdentifierNode parent_identifiers){
			
			var current_namespace = new NamespaceNode();
			current_namespace.identifier = tree.identifier;
			current_namespace.using_array = tree.using_array;
			current_namespace.types_declaration_array = tree.types_declaration_array;

			int parent_pos = -1;
			var parent_name = parent_identifiers.ToString();
			var i = 0;
			foreach(var nsp in lista){
				if(nsp.ToString() == parent_name)
					parent_pos = i;
				i++;
			}

			current_namespace.parent_position = parent_pos;
			current_namespace.identifier.identifiers.InsertRange(0, parent_identifiers.identifiers);

			lista.Add(current_namespace);


			foreach(var nspname in tree.namespace_array ){
				GetNamespaces(ref lista, nspname, current_namespace.identifier);
			}
		}

		private static void PutParentInformation(ref List<NamespaceNode> nsp, List<Token> parent_identifiers, int parent_pos){
			foreach(var nspname in nsp ){
				nspname.identifier.identifiers.InsertRange(0, parent_identifiers);
				nspname.parent_position = parent_pos;
			}
		}
	}
}