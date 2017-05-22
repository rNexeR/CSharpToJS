using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CStoJS.Inputs;
using CStoJS.LexerLibraries;
using CStoJS.ParserLibraries;
using CStoJS.Exceptions;

namespace CStoJS.MsTests
{
    [TestClass]
    public class StatementsTests
    {
        [TestMethod]
        public void TestClassWithLocalStamentsInBody()
        {
            var inputString = new InputString(@"
public class kevin : Nexer{
    public void method(int a){
        var hola = 5;
        int uno, dos, tres = 5;

    }
}
");
            var lexer = new Lexer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithMultipleLocalStamentsInBody()
        {
            var txt = System.IO.File.ReadAllText(@"/home/rnexer/DEV/Compi/CSharpToJS/MsTests/SyntaxTests/StatementsTests/local_variable_test_file.txt");
            var inputString = new InputString(txt);
            var lexer = new Lexer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithStamentsInBody()
        {
            var txt = System.IO.File.ReadAllText(@"/home/rnexer/DEV/Compi/CSharpToJS/MsTests/SyntaxTests/StatementsTests/test_statements.txt");
            var inputString = new InputString(txt);
            var lexer = new Lexer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithMaybeEmptyBlockInBody()
        {
            var txt = @"
public class kevin : Nexer{
    public method(int a){
    }

    public method(int a) : base(a){
        {
            if(a==10){
                return a;
                {
                    for(;;){}
                }
            }
        }
    }
}
";
            var inputString = new InputString(txt);
            var lexer = new Lexer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }

        [TestMethod]
        public void TestClassWithStatementExpressionInBody()
        {
            var txt = @"
public class kevin : Nexer{
    public method(int a){
    }

    public method(int a) : base(a){
        this.local = a;
        ++x;
        x++;
        (Persona)new Persona();
        5;
        (5+A).CompareTo(10);
        this.prototype.jamon(a,a).value;
    }
}
";
            var inputString = new InputString(txt);
            var lexer = new Lexer(inputString);
            var parser = new Parser(lexer);
            parser.parse();
        }
    }
}
