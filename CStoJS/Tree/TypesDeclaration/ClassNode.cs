using System;
using System.Collections.Generic;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ClassNode : TypeDeclarationNode
    {
        public List<IdentifierNode> inherit;
        public List<MethodNode> methods;
        public List<FieldNode> fields;
        public List<ConstructorNode> constructors;
        public bool isAbstract; 
        public ClassNode(){
            this.type = "class";
            this.inherit = new List<IdentifierNode>();
            this.methods = new List<MethodNode>();
            this.fields = new List<FieldNode>();
            this.constructors = new List<ConstructorNode>();
            this.isAbstract = false;
        }

        public override void EvaluateSemantic(Semantic.API api){
            // var context_manager = new ContextManager(api);
            // var parent_nsp = api.namespaces[this.namespace_index].identifier.ToString();
            // var class_name = $"{parent_nsp}.{this.identifier.ToString()}";
            // context_manager.Push(new Context(ContextType.CLASS_CONTEXT, class_name), class_name);
            // this.EvaluateInheritance(api);
            // this.EvaluateFieldsSemantic(api, context_manager);
            // this.EvaluateConstructors(api, context_manager);
            // this.EvaluateMethods(api, context_manager);
        }

        private void EvaluateInheritance(API api)
        {
            throw new NotImplementedException();
        }

        private void EvaluateMethods(API api, ContextManager context_manager)
        {
            throw new NotImplementedException();
        }

        private void EvaluateConstructors(API api, ContextManager context_manager)
        {
            throw new NotImplementedException();
        }

        private void EvaluateFieldsSemantic(API api, ContextManager context_manager)
        {
            throw new NotImplementedException();
        }
    }
}