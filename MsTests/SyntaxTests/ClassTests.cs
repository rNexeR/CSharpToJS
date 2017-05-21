using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Exceptions;

namespace CStoJS.MsTests
{
    [TestClass]
    public class ClassTests
    {
        [TestMethod]
        public void ClassOutsideNamespace(){
            var input = new InputString(@"public class Hola{
                void sayHello( int time ){}
                int sum(int a, int b);
            }");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void ClassInsideNamespace(){
            var input = new InputString(@"
            namespace Hi{
                public class Hola{
                    void sayHello(){}
                    int sum(int a, int b);
                }
            }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void ClassBaseOther(){
            var input = new InputString(@"
            namespace Hi{
                public class Hola : Greetings, SpanishGreetings{
                    private int x, y, z;
                    public Hola(){}
                    void sayHello(){}
                    int sum(int a, int b);
                }
            }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void ComplexTest(){
            var input = new InputString(@"
            using CompilerLibrary; using Microsoft.Visual.Tester.Oke;
                namespace CompilerTests { 
                    using alter.test.cli; 
                    namespace SecondCompiler { 
                        using alter.test.cli2; 
                        using alter.test.boring; 
                    }
                    public enum Numbers
                    {
                    }
                    public enum Letters
                    {
                        A, B, C, D, E, F, G
                    } 
                }  
                
                namespace ThirdCompiler { 
                    public class Test {
                        public Test()
                        {
                        }
                        public void MethodOne()
                        {
                        }
                        public int CalculateTwo()
                        {
                        }
                        
                        public bool GetTheTruth(bool WasLie, bool WasNothing, int LengthOf)
                        {
                        }
                        private int count;
                    } 
                    
                    public abstract class Due {
                        
                    }
                    public class Quattro : Due {
                    }
                    public interface Nuovo {
                        void Check();
                        int Count();
                        Due Objeto();
                    }
                    public interface Giovane : Nuovo {
                        bool Equal();
                    } 
                }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void VariousClasss(){
            var input = new InputString(@"
            namespace Hi{
                public class Hola{
                    void sayHello(){}
                    int sum(int a, int b);
                }

                public class Hola2{
                    void sayHello();
                    int sum(int a, int b);
                }
            }
            public class Hola{
                void sayHello();
                int sum(int a, int b);
            }

            public class Hola2{
                void sayHello();
                int sum(int a, int b);
            }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        [ExpectedException( typeof(SyntaxException) )]
        public void ClassWithoutBraceClose(){
            var input = new InputString(@"
                public class Hola{
                    void sayHello();
                    int sum(int a, int b);
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }
    }
}