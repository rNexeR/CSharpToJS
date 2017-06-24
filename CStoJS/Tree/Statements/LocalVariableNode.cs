using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class LocalVariableNode : StatementNode
    {
        public TypeDeclarationNode type;
        public IdentifierNode identifier;
        public VariableInitializer assignation;
        public LocalVariableNode()
        {

        }

        public LocalVariableNode(TypeDeclarationNode type, IdentifierNode identifier)
        {
            this.identifier = identifier;
            this.type = type;
        }

        public LocalVariableNode(TypeDeclarationNode type, IdentifierNode identifier, VariableInitializer assignation) : this(type, identifier)
        {
            this.assignation = assignation;
        }

        public override TypeDeclarationNode EvaluateSemantic(API api, ContextManager context_manager)
        {
            var _usings = context_manager.GetCurrentNamespaceUsings();
            var type_name = this.type.ToString();
            if (!(this.type is ArrayType) && !(this.type is VarType))
            {
                type_name = Utils.GetClassName(type.ToString(), _usings, api);
                if (!api.TypeDeclarationExists(type_name))
                    throw new SemanticException($"Type of field {this.identifier} not found.", this.type.identifier.identifiers[0]);
                this.type = api.GetTypeDeclaration(type_name);
            }
            if (this.assignation == null)
                context_manager.AddVariableToCurrentContext(this.identifier.ToString(), this.type);
            var rules = new Dictionary<string, TypeDeclarationNode>();
            rules["FloatType,IntType"] = new FloatType();
            rules["IntType,CharType"] = new IntType();
            if (this.assignation != null)
            {
                var assign_ret = this.assignation.EvaluateType(api, context_manager);
                context_manager.AddVariableToCurrentContext(this.identifier.ToString(), assign_ret);
                if (assign_ret.ToString() != type.ToString() && !rules.ContainsKey($"{type.ToString()},{assign_ret}"))
                {
                    if (((this.type is ClassNode) || this.type.ToString() == "StringType") && assign_ret.ToString() == "NullType")
                    {
                        return null;
                    }
                    else if (Utils.AreEquivalentsTypes(this.type, assign_ret, _usings, api))
                    {
                        // }else if((this.type is ClassNode) && (assign_ret is ClassNode)){
                        //     var assign_type = Utils.GetClassName(assign_ret.identifier.ToString(), _usings, api);
                        //     if(Utils.IsChildOf(type_name, assign_type, api))
                        //         return null;
                        // }else if(this.type is ArrayType && assign_ret is ArrayType && 
                        //     ( (this.type as ArrayType).arrayOfArrays == (assign_ret as ArrayType).arrayOfArrays  || (this.type as ArrayType).dimensions == (assign_ret as ArrayType).dimensions )){
                        //     var assign_type = Utils.GetClassName((assign_ret as ArrayType).baseType.identifier.ToString(), _usings, api);
                        //     var var_type = Utils.GetClassName((this.type as ArrayType).baseType.identifier.ToString(), _usings, api);
                        //     if(Utils.IsChildOf(var_type, assign_type, api))
                        //         return null;
                        return null;
                    }
                    else if (this.type is VarType)
                        return null;
                    throw new SemanticException($"Assignation expression ({assign_ret}) mismatch with field type ({type}).", identifier.identifiers[0]);
                }
            }
            return null;
        }
    }
}