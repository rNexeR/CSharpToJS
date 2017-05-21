using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Exceptions;

namespace MsTests.SyntaxTests
{
    [TestClass]
    public class InterfacesTests
    {
        [TestMethod]
        public void InterfaceOutsideNamespace(){
            var input = new InputString(@"public interface Hola{
                void sayHello();
                int sum(int a, int b);
            }");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void InterfaceInsideNamespace(){
            var input = new InputString(@"
            namespace Hi{
                public interface Hola{
                    void sayHello();
                    int sum(int a, int b);
                }
            }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void VariousInterfaces(){
            var input = new InputString(@"
            namespace Hi{
                public interface Hola{
                    void sayHello();
                    int sum(int a, int b);
                }

                public interface Hola2{
                    void sayHello();
                    int sum(int a, int b);
                }
            }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        [ExpectedException( typeof(SyntaxException) )]
        public void InterfaceWithoutBraceClose(){
            var input = new InputString(@"
                public interface Hola{
                    void sayHello();
                    int sum(int a, int b);
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }
    }
}