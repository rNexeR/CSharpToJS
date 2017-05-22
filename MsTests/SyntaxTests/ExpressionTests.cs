using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Exceptions;

namespace MsTests.SyntaxTests
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void ComplexExpressionTest(){
            var txtContent =  System.IO.File.ReadAllText(@"/home/rnexer/DEV/Compi/CSharpToJS/MsTests/SyntaxTests/Expression.txt");
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [TestMethod]
        public void SimpleExpressionTest(){
            var txtContent = @"
            public class kevin : Javier
            {
                int before = isVisible && IsHere;
                int before = (isVisible && IsHere);
                
            }
            ";
            var input = new InputString(txtContent);
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }
    }
}