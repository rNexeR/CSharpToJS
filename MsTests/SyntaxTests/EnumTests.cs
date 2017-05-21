using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Exceptions;

namespace CStoJS.MsTests
{
    [TestClass]
    public class EnumTests
    {
        //No puede ser private, ni protected, solo public
        [TestMethod]
        public void EnumOutsideNamespace(){
            var input = new InputString(@"public enum Hola{
                HOLA,
                ADIOS
            }");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void EnumInsideNamespace(){
            var input = new InputString(@"
            namespace Hi{
                public enum Hola{
                    HOLA,
                    ADIOS
                }
            }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void EnumWithValue(){
            var input = new InputString(@"
            namespace Hi{
                public enum Hola{
                    HOLA = 0,
                    ADIOS = 5
                }
            }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void EnumWithCommaAfterLastEnumeration(){
            var input = new InputString(@"
            namespace Hi{
                public enum Hola{
                    HOLA,
                    ADIOS,
                }
            }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void VariousEnums(){
            var input = new InputString(@"
            namespace Hi{
                public enum Hola{
                    HOLA,
                    ADIOS,
                }

                public enum Hola2{
                    HOLA,
                    ADIOS,
                }
            }
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        [ExpectedException( typeof(SyntaxException) )]
        public void EnumWithoutBraceClose(){
            var input = new InputString(@"
                public enum Hola{
                    HOLA,
                    ADIOS
            ");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

    }
}