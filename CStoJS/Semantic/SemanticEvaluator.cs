using System;
using System.Collections.Generic;
using CStoJS.Inputs;
using CStoJS.LexerLibraries;
using CStoJS.ParserLibraries;
using CStoJS.Tree;

namespace CStoJS.Semantic
{
    public class SemanticEvaluator
    {
        public List<NamespaceNode> trees;
        public API api;
        public ContextManager context_manager;
        private List<string> files;
        private bool parse_errors;

        public SemanticEvaluator()
        {
            this.trees = new List<NamespaceNode>();
        }

        public SemanticEvaluator(List<string> files)
        {
            this.files = files;
            this.parse_errors = false;
            this.trees = new List<NamespaceNode>();
            if (!this.ParseFiles())
            {
                this.parse_errors = true;
                return;
            }
            // try
            // {
            this.api = new API(trees);
            this.context_manager = new ContextManager(api);
            // }catch(Exception ex){
            //     Console.WriteLine($"{ex.Message}");
            // }
        }

        private bool ParseFiles()
        {
            AddSystemClasses();
            foreach (var file in files)
            {
                try
                {
                    var txtContent = System.IO.File.ReadAllText(file);
                    var input = new InputString(txtContent);
                    var lexer = new Lexer(input);
                    var parser = new Parser(lexer);
                    trees.Add(parser.parse());
                    // SerializeTree(tree);
                    // var namespaces = SemanticUtils.GetNamespaces(tree);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{file}: {ex.Message}");
                    return false;
                }

            }
            return true;
        }

        private void AddSystemClasses()
        {
            var txtContent = @"
            namespace System{
                namespace IO{
                    public class TextWriter{
                        public static void WriteLine(){}
                    }
            
                    public class TextReader{
                        public static string ReadLine(){}
                    }
                }

                public class Console{
                    public static System.IO.TextWriter Out;
                    public static System.IO.TextReader In;
                    public static void WriteLine(){}
                    public static string ReadLine(){}
                }
                
                public class Object{
                    public virtual string ToString(){}
                }
            
                public class IntType{
                    public override string ToString(){}
                    public static int Parse(string s){}
                    public static int TryParse(string s, int out){}
                }
                
                public class CharType{
                    public override string ToString(){}
                    public static int Parse(string s){}
                    public static int TryParse(string s, char out){}
                }
            
                public class DictionaryTypeNode{
                    public override string ToString(){}
                }

                public class FloatType{
                    public override string ToString(){}
                    public static int Parse(string s){}
                    public static int TryParse(string s, float out){}
                }
                public class StringType{
                    public override string ToString(){}
                }
                public class VarType{
                    public override string ToString(){}
                }
            
                public class VoidType{
                    public override string ToString(){}
                }
            }
            ";
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);
            trees.Add(parser.parse());
        }

        public void Evaluate()
        {
            if (parse_errors)
            {
                return;
            }

            var i = 0;
            foreach (var nsp in this.trees)
            {
                try
                {
                    nsp.EvaluateSemantic(api);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"->{this.files[i-1]}: {ex.Message}");
                    return;
                }
                i++;
            }

        }
    }
}