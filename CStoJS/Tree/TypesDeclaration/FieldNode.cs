using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class FieldNode
    {
        public TypeDeclarationNode type;
        public IdentifierNode identifier;
        public EncapsulationNode encapsulation;
        public Token modifier;
        public VariableInitializer assignment;

        public FieldNode()
        {

        }

        public FieldNode(TypeDeclarationNode type, IdentifierNode identifier, EncapsulationNode encapsulation, Token modifier)
        {
            this.type = type;
            this.identifier = identifier;
            this.encapsulation = encapsulation;
            if (this.encapsulation.token == null)
                this.encapsulation = new EncapsulationNode(new Token(TokenType.PRIVATE_KEYWORD, "private", 0, 0));
            this.modifier = modifier;
        }

        public FieldNode(TypeDeclarationNode type, IdentifierNode identifier, EncapsulationNode encapsulation, Token modifier, VariableInitializer assignment) : this(type, identifier, encapsulation, modifier)
        {
            this.assignment = assignment;
        }

        public override string ToString()
        {
            return this.identifier.ToString();
        }

        public void Evaluate(API api, ContextManager context_manager)
        {
            var _usings = context_manager.GetCurrentNamespaceUsings();
            var type_name = this.type.ToString();
            if (!(this.type is ArrayType))
            {
                type_name = Utils.GetClassName(type.ToString(), _usings, api);
                if (!api.TypeDeclarationExists(type_name))
                    throw new SemanticException($"Type of field {this.identifier} not found.", this.type.identifier.identifiers[0]);
                this.type = api.GetTypeDeclaration(type_name);
            }
            if (this.modifier != null && this.modifier.lexema == "static")
                context_manager.SetStaticContext();
            var rules = new Dictionary<string, TypeDeclarationNode>();
            rules["FloatType,IntType"] = new FloatType();
            rules["IntType,CharType"] = new IntType();
            if (this.assignment != null)
            {
                var assign_ret = this.assignment.EvaluateType(api, context_manager);
                if (assign_ret.ToString() != type.ToString() && !rules.ContainsKey($"{type.ToString()},{assign_ret}") && !Utils.AreEquivalentsTypes(this.type, assign_ret, _usings, api))
                {
                    if (((this.type is ClassNode) || this.type.ToString() == "StringType") && assign_ret.ToString() == "NullType")
                        return;
                    throw new SemanticException($"Assignation expression ({assign_ret}) mismatch with field type ({type}).", identifier.identifiers[0]);
                }
            }
        }
    }
}