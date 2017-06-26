using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ForeachStatementNode : EmbeddedStatementNode
    {
        public LocalVariableNode localVariable;
        public ExpressionNode collection;
        private StatementNode body;

        public ForeachStatementNode(){

        }

        public ForeachStatementNode(LocalVariableNode localVariable, ExpressionNode collection) : base(){
            this.localVariable = localVariable;
            this.collection = collection;
        }

        public ForeachStatementNode(LocalVariableNode localVariable, ExpressionNode collection, StatementNode body) : this(localVariable, collection)
        {
            this.body = body;
        }

        public override TypeDeclarationNode EvaluateSemantic(Semantic.API api, Semantic.ContextManager context_manager){
            context_manager.Push(new Context(ContextType.FOREACH_CONTEXT));
            // var type = localVariable.assignation.EvaluateType(api, context_manager);
            
            var collection_type = this.collection.EvaluateType(api, context_manager);

            if(!(collection_type is ArrayType))
                throw new SemanticException("Expression is not a collection");
            
            var col_arr = collection_type as ArrayType;
            TypeDeclarationNode iterator = col_arr.baseType;

            if(col_arr.dimensions == 0){
                if(col_arr.arrayOfArrays > 1){
                    var temp = new ArrayType();
                    temp.arrayOfArrays = col_arr.arrayOfArrays -1;
                    temp.baseType = col_arr.baseType;
                    iterator = temp;
                }
            }

            if(!(localVariable.type is VarType) && localVariable.type.ToString() != iterator.ToString())
                throw new SemanticException($"Cannot assign {iterator} to a {localVariable.type} variable", localVariable.identifier.identifiers[0]);


            context_manager.AddVariableToCurrentContext(localVariable.identifier.ToString(), iterator);

            var ret = body.EvaluateSemantic(api, context_manager);
            context_manager.Pop();
            return ret;
        }

        public override void GenerateCode(Outputs.IOutput output, API api){
            output.WriteString($"\t\t\tfor(let {this.localVariable.identifier} of ");
            this.collection.GenerateCode(output, api);
            output.WriteString(")");
            if(this.body != null){
                output.WriteStringLine("{");
                this.body.GenerateCode(output, api);
                output.WriteStringLine("\t\t\t}");
            }else{
                output.WriteStringLine(";");
            }
        }
    }
}