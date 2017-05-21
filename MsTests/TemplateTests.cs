using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.ParserLibraries;
using CStoJS.Exceptions;

namespace CStoJS.MsTests
{
    [TestClass]
    public class ExampleTests
    {
        [TestMethod]
        [ExpectedException ( typeof(NotImplementedException) )]
        public void ThrowsError(){
            // var input = new InputString("namespace test.test1.test2{\n}");
            // var lexer = new Lexer(input);
            // var parser = new Parser(lexer);

            // parser.parse();
            throw new NotImplementedException();
        }
    }
}