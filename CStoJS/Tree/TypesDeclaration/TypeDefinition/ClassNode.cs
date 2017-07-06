using System;
using System.Collections.Generic;
using CStoJS.Exceptions;
using CStoJS.LexerLibraries;
using CStoJS.Semantic;

namespace CStoJS.Tree
{
    public class ClassNode : TypeDefinitionNode
    {
        public ClassNode() : base()
        {
            this.type = "class";
            this.isAbstract = false;
            this.encapsulation_modifier = new EncapsulationNode(new Token(TokenType.PRIVATE_KEYWORD, "private", 0, 0));
        }

        public override void EvaluateSemantic(Semantic.API api)
        {
            Console.WriteLine($"Evaluating class {this.identifier}");
            var context_manager = new ContextManager(api);
            var parent_nsp = api.namespaces[this.namespace_index].ToString();
            var class_name = parent_nsp == "" ? $"{this.identifier.ToString()}" : $"{parent_nsp}.{this.identifier.ToString()}";
            context_manager.Push(new Context(ContextType.CLASS_CONTEXT, class_name), class_name);
            // return;
            this.EvaluateInheritance(api, context_manager);
            this.EvaluateFieldsSemantic(api, context_manager);
            this.EvaluateConstructors(api, context_manager);
            this.EvaluateMethods(api, context_manager);
            // var parents = Utils.GetParentsNames(context_manager);
        }

        private void EvaluateInheritance(API api, ContextManager ctx_man)
        {
            var base_class_found = false;
            foreach (var base_type in inherit)
            {
                var _usings = api.namespaces[this.namespace_index].using_array;
                // _usings.Insert(0, new UsingNode(api.namespaces[this.namespace_index].identifier));
                var found = false;
                foreach (var _using in _usings)
                {
                    var base_name = _using.ToString() == "" ? base_type.ToString() : $"{_using.ToString()}.{base_type.ToString()}";
                    if (api.TypeDeclarationExists(base_name))
                    {
                        var type = api.GetTypeDeclaration(base_name);
                        if (type.encapsulation_modifier.token != null && type.encapsulation_modifier.token.lexema != "public")
                        {
                            // var nsp_parent = api.namespaces[type.namespace_index].ToString();
                            // var current_nsp = api.namespaces[this.namespace_index].ToString();
                            // if(nsp_parent != current_nsp)
                            throw new SemanticException("Base class is inaccessible due to its protection level.", base_type.identifiers[0]);
                        }
                        if (type is ClassNode)
                        {
                            if (base_class_found)
                                throw new SemanticException("Cannot have multiple base classes", base_type.identifiers[0]);
                            base_class_found = true;
                        }
                        found = true;
                        break;
                    }
                }
                if (!found)
                    throw new SemanticException($"Base Class or Interface not found. Base name {base_type.ToString()}", base_type.identifiers[0]);
            }

            var to_implements = Utils.MethodsToImplements(ctx_man);

            foreach (var method in this.methods)
            {
                if (to_implements.ContainsKey(method.ToString()) && to_implements[method.ToString()].ToString() == method.returnType.ToString())
                {
                    to_implements.Remove(method.ToString());
                }
            }

            if (to_implements.Count > 0)
                throw new SemanticException($"Class {this.identifier.ToString()} doesn't implements all methods from his parents.", this.identifier.identifiers[0]);
        }

        private void EvaluateMethods(API api, ContextManager context_manager)
        {
            foreach (var method in this.methods)
            {
                if (method.returnType.ToString() != "VoidType")
                {
                    var _usings = api.namespaces[this.namespace_index].using_array;
                    var found = false;
                    foreach (var _using in _usings)
                    {
                        var type_name = _using.ToString() == "" ? method.returnType.ToString() : $"{_using.ToString()}.{method.returnType.ToString()}";
                        if (api.TypeDeclarationExists(type_name))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        throw new SemanticException($"Return Type Class not found. Type name {method.returnType.ToString()}", method.returnType.identifier.identifiers[0]);
                }
                method.EvaluateSemantic(api, context_manager, method.returnType);
            }
        }

        private void EvaluateConstructors(API api, ContextManager context_manager)
        {
            foreach (var ctor in this.constructors)
            {
                ctor.Evaluate(api, context_manager, this.identifier.ToString());
            }
        }

        private void EvaluateFieldsSemantic(API api, ContextManager context_manager)
        {
            context_manager.SetStaticContext();
            foreach (var field in this.fields)
            {
                field.Evaluate(api, context_manager);
            }
            context_manager.UnsetStaticContext();
        }

        public override void GenerateCode(Outputs.IOutput output, API api)
        {
            if(generated)
                return;
            var nsp_name = api.namespaces[this.namespace_index].identifier.ToString();
            var full_name = Utils.GetFullName(nsp_name, this.identifier.ToString());

            Utils.PrintDebug($"Generating class {full_name}");

            var _usings = api.namespaces[this.namespace_index].using_array;
            var base_class = "GeneratedCode.Object";
            foreach (var parent in this.inherit)
            {
                var p_name = Utils.GetClassName(parent.ToString(), _usings, api);
                var parent_type = api.GetTypeDeclaration(p_name);
                if (parent_type is ClassNode)
                {
                    base_class = $"GeneratedCode.{p_name}";
                    parent_type.GenerateCode(output, api);
                    output.WriteStringLine("");
                }
            }

            output.WriteStringLine($"{full_name} = class extends {base_class} {{");
            AddClassFieldsCode(output, api);
            AddConstructorsCode(output, api);
            AddClassMethodsCode(output, api);
            output.WriteStringLine("};");

            AddStaticClassFieldsCode(output, full_name, api);
            this.generated = true;
        }

        private void AddClassMethodsCode(Outputs.IOutput output, API api)
        {
            foreach (var method in this.methods)
            {
                output.WriteStringLine("");
                var method_name = Utils.GetMethodName(method.identifier.ToString(), method);
                var modifier = method.modifier is null ? "" : method.modifier.lexema == "static" ? "static " : "";
                output.WriteString($"\t{modifier}{method_name}");
                if(method.body != null){
                    output.WriteStringLine("{");
                    method.body.GenerateCode(output, api);
                    output.WriteStringLine("\t};");
                }else
                    output.WriteStringLine("{};");
            }
        }

        private void AddConstructorsCode(Outputs.IOutput output, API api)
        {
            foreach (var ctor in this.constructors)
            {
                output.WriteStringLine("");
                var ctor_name = Utils.GetCtorName(this.identifier.ToString(), ctor);
                output.WriteStringLine($"\t{ctor_name} {{");
                // if(ctor.initializer != null){
                //     output.WriteString("\t\t\tsuper(");
                //     output.WriteString("");
                //     output.WriteStringLine(");");
                // }
                ctor.body.GenerateCode(output, api);
                output.WriteStringLine($"\t}};");
            }
        }

        private void AddStaticClassFieldsCode(Outputs.IOutput output, string full_name, API api)
        {
            foreach (var field in this.fields)
            {
                if (!(field.modifier is null))
                {
                    Utils.PrintDebug($"Static class field {full_name}.{field}");
                    output.WriteString($"{full_name}.{field} = ");
                    if(field.assignment != null)
                        field.assignment.GenerateCode(output, api);
                    else
                        output.WriteString("null");
                    output.WriteString(";");

                }
            }
            output.WriteStringLine("");
        }

        private void AddClassFieldsCode(Outputs.IOutput output, API api)
        {
            output.WriteStringLine("");
            output.WriteStringLine("\tconstructor() {");
            output.WriteStringLine("\t\tsuper();");
            foreach (var field in this.fields)
            {
                if (field.modifier is null)
                {
                    Utils.PrintDebug($"Class field {field}");
                    output.WriteString($"\t\tthis.{field} = ");
                    if (field.assignment is null)
                        output.WriteString("null");
                    else
                    {
                        if (field.assignment.returnType.ToString() == "CharType" && field.type.ToString() == "IntType")
                        {
                            output.WriteString("CharToInt(");
                            field.assignment.GenerateCode(output, api);
                            output.WriteString(")");
                        }
                        else
                            field.assignment.GenerateCode(output, api);
                    }
                    output.WriteStringLine(";");
                }
            }
            output.WriteStringLine(Utils.ctors_caller);
            output.WriteStringLine("\t};");
        }
    }
}