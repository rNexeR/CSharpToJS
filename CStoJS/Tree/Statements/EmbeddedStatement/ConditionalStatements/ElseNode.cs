using System;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ElseNode
    {
        public EmbeddedStatementNode body;

        public ElseNode(){

        }

        public ElseNode(EmbeddedStatementNode body){
            this.body = body;
        }

        public TypeDeclarationNode EvaluateSemantic(API api, ContextManager context_manager)
        {
            return body.EvaluateSemantic(api, context_manager);
        }
    }
}