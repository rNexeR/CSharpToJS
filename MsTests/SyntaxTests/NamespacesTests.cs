using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Exceptions;

namespace CStoJS.MsTests
{
    [TestClass]
    public class NamespacesTests
    {
        [TestMethod]
        public void EmptyNamespace(){
            var input = new InputString("namespace test.test1.test2{\n}");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void MoreThanOneNamespaces(){
            var input = new InputString("namespace test{\n} namespace test2{\n} namespace test3{\n}");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void NamespacesInsideNamespace(){
            var input = new InputString(@"namespace test{
                namespace test1{
                    namespace test2{}
                }
            } 
            namespace test3{}
            namespace test4{}");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        [ExpectedException( typeof( SyntaxException ) )]
        public void BraceCloseExpected(){
            var input = new InputString("namespace test{");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }
    }
}