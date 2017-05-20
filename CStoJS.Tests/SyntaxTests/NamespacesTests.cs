using System;
using Xunit;

namespace CStoJS.Tests
{
    public class NamespacesTests
    {
        [Fact]
        public void EmptyNamespace(){
            var input = new InputString("namespace test.test1.test2{\n}");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [Fact]
        public void MoreThanOneNamespaces(){
            var input = new InputString("namespace test{\n}; namespace test2{\n};namespace test3{\n}");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [Fact]
        public void NamespacesInsideNamespace(){
            var input = new InputString(@"namespace test{
                namespace test1{
                    namespace test2{}
                }
            }; namespace test2{};namespace test3{}");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            parser.parse();
        }

        [Fact]
        public void BraceCloseExpected(){
            var input = new InputString("namespace test{\n");
            var lexer = new Lexer(input);
            var parser = new Parser(lexer);

            Exception ex = Assert.Throws<SyntaxException>(() => parser.parse());

            Assert.Equal("BRACE_CLOSE expected.", ex.Message);
        }
    }
}