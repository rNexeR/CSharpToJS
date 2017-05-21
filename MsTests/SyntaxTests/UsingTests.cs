using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Exceptions;

namespace MsTests.SyntaxTests
{   
    [TestClass]
    public class UsingTests
    {
        [TestMethod]
        public void UsingIdentifier(){
            var input = new InputString("using System;");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void UsingIdentifierList(){
            var input = new InputString("using System.Hola.Text;");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        [ExpectedException( typeof(SyntaxException) )]
        public void UsingWithoutComma(){
            var input = new InputString("using System");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void UsingListInsideNamespace(){
            var input = new InputString(@"namespace nxr{
                using nxrnsp;
                using nxr2nsp.say;
            }");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }
    }
}