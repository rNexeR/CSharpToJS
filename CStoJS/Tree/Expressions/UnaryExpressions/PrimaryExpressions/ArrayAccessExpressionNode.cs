using System;
using System.Collections.Generic;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ArrayAccessExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode identifier;
        public List<ArrayAccessNode> indexes;

        public ArrayAccessExpressionNode(List<ArrayAccessNode> indexes)
        {
            this.indexes = indexes;
        }

        public ArrayAccessExpressionNode(){
            
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            return null;
        }
    }
}