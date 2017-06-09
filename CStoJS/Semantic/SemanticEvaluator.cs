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
                    Console.WriteLine($"->{this.files[i]}: {ex.Message}");
                    return;
                }
                i++;
            }

        }
    }
}