using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Exceptions;

namespace CStoJS.MsTests
{
    [TestClass]
    public class AllInOne
    {
        [TestMethod]
        public void AllInOneTest1(){
            var txtContent =  System.IO.File.ReadAllText(@"/home/rnexer/DEV/Compi/CSharpToJS/MsTests/CompleteExample.txt");
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void AllInOneTest2(){
            var txtContent =  System.IO.File.ReadAllText(@"/home/rnexer/DEV/Compi/CSharpToJS/MsTests/CompleteExample2.txt");
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void AllInOneTest3(){
            var txtContent =  System.IO.File.ReadAllText(@"/home/rnexer/DEV/Compi/CSharpToJS/MsTests/CompleteExample3.txt");
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void AllInOneTest4(){
            var txtContent =  System.IO.File.ReadAllText(@"/home/rnexer/DEV/Compi/CSharpToJS/MsTests/compiiiss1.txt");
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }
    }
}