using System;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class JumpStatementNode : EmbeddedStatementNode
    {
        public Token identifier;
        public ExpressionNode optionalExpression;

        public JumpStatementNode()
        {

        }

        public JumpStatementNode(Token identifier, ExpressionNode optionalExpression)
        {
            this.identifier = identifier;
            this.optionalExpression = optionalExpression;
        }

        public override TypeDeclarationNode EvaluateSemantic(API api, ContextManager context_manager)
        {
            //checks contexts if the jump statement is break or continue
            if (identifier.type == TokenType.RETURN_KEYWORD)
            {
                return EvaluateReturnSemantic(api, context_manager);
            }
            else if (identifier.type == TokenType.BREAK_KEYWORD)
            {
                return EvaluateBreakSemantic(api, context_manager);
            }
            else
            {
                return EvaluateContinueSemantic(api, context_manager);
            }
        }

        private TypeDeclarationNode EvaluateContinueSemantic(API api, ContextManager context_manager)
        {
            if (
                !context_manager.HasContextType(ContextType.FOR_CONTEXT) &&
                !context_manager.HasContextType(ContextType.FOREACH_CONTEXT) &&
                !context_manager.HasContextType(ContextType.WHILE_CONTEXT) &&
                !context_manager.HasContextType(ContextType.DO_CONTEXT)
                )
                throw new SemanticException("Continue found in a no cycle context.", this.identifier);

             if (this.optionalExpression is null)
                return null;
            
            throw new SemanticException("Continue cannot have right expression.", this.identifier);
        }

        private TypeDeclarationNode EvaluateBreakSemantic(API api, ContextManager context_manager)
        {
            if (
                !context_manager.HasContextType(ContextType.FOR_CONTEXT) &&
                !context_manager.HasContextType(ContextType.FOREACH_CONTEXT) &&
                !context_manager.HasContextType(ContextType.WHILE_CONTEXT) &&
                !context_manager.HasContextType(ContextType.DO_CONTEXT) &&
                !context_manager.HasContextType(ContextType.SWITCH_CONTEXT)
                )
                throw new SemanticException("Break found in a no cycle or switch context.", this.identifier);
             if (this.optionalExpression is null)
                return null;
            
            throw new SemanticException("Break cannot have right expression.", this.identifier);
        }

        private TypeDeclarationNode EvaluateReturnSemantic(API api, ContextManager context_manager)
        {
            if (!context_manager.HasContextType(ContextType.METHOD_CONTEXT) && !context_manager.HasContextType(ContextType.CONSTRUCTOR_CONTEXT))
                throw new SemanticException("Return found in a no method or constructor context.", this.identifier);

            if (this.optionalExpression is null)
                return new VoidType();
            else
            {
                if (!context_manager.HasContextType(ContextType.METHOD_CONTEXT))
                    throw new SemanticException("Return expression found in a no method context.", this.identifier);
                return optionalExpression.EvaluateType(api, context_manager);
            }
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            output.WriteString($"\t\t{this.identifier.lexema}");
            if(this.optionalExpression != null){
                output.WriteString(" ");
                this.optionalExpression.GenerateCode(output, api);
            }
            output.WriteStringLine(";");
        }
    }
}