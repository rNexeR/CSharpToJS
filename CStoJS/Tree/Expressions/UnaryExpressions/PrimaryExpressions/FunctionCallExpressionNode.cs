using System;
using System.Collections.Generic;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class FunctionCallExpressionNode : PrimaryExpressionNode
    {
        public ExpressionNode identifier;
        public List<ArgumentNode> args;

        public FunctionCallExpressionNode(List<ArgumentNode> args)
        {
            this.args = args;
        }

        public FunctionCallExpressionNode(){
            
        }

        public override TypeDeclarationNode EvaluateType(API api, ContextManager ctx_man)
        {
            return null;
        }
    }
}