using System;
using Xunit;

namespace CStoJS.Tests
{
    public class OperatorsAndSymbolsTest
    {

        [Fact]
        public void AssignationOperator()
        {
            var input = new InputString("=\0");
            var lexer = new Lexer(input);

            Assert.True(TokenType.OP_ASSIGN == lexer.GetNextToken().type);
        }

        [Fact]
        public void ConditionalEqualOperator(){
            var input = new InputString("==\0");
            var lexer = new Lexer(input);

            Assert.True(TokenType.OP_CONDITIONAL_EQUAL == lexer.GetNextToken().type);
        }

        [Fact]
        public void LessThanOperator(){
            var input = new InputString("<\0");
            var lexer = new Lexer(input);

            Assert.True(TokenType.OP_LESS_THAN == lexer.GetNextToken().type);
        }

        [Fact]
        public void LessEqualThanOperator(){
            var input = new InputString("<=\0");
            var lexer = new Lexer(input);

            Assert.True(TokenType.OP_LESS_EQUAL_THAN == lexer.GetNextToken().type);
        }

        [Fact]
        public void ShiftLeftOperator(){
            var input = new InputString("<<\0");
            var lexer = new Lexer(input);

            Assert.True(TokenType.OP_BITS_SHIFT_LEFT == lexer.GetNextToken().type);
        }

        [Fact]
        public void GreaterThanOperator(){
            var input = new InputString(">\0");
            var lexer = new Lexer(input);

            Assert.True(TokenType.OP_GREATER_THAN == lexer.GetNextToken().type);
        }

        [Fact]
        public void GreaterEqualThanOperator(){
            var input = new InputString(">=\0");
            var lexer = new Lexer(input);

            Assert.True(TokenType.OP_GREATER_EQUAL_THAN == lexer.GetNextToken().type);
        }

        [Fact]
        public void ShiftRightOperator(){
            var input = new InputString(">>\0");
            var lexer = new Lexer(input);

            Assert.True(TokenType.OP_BITS_SHIFT_RIGHT == lexer.GetNextToken().type);
        }

        [Fact]
        public void ArithmeticOperators(){
            var input = new InputString("+ - * / %\0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.OP_SUM, TokenType.OP_SUBSTRACT, TokenType.OP_MULTIPLICATION, TokenType.OP_DIVISION, TokenType.OP_MODULO};

            var current = lexer.GetNextToken().type;
            var i = 0;
            while(current != TokenType.EOF  && i < expectedTypes.Length){
                Assert.True(expectedTypes[i++] == current);
                current = lexer.GetNextToken().type;
            }

            Assert.True(i == expectedTypes.Length);
        }

        [Fact]
        public void BitwiseOperators(){
            var input = new InputString("<< >> ~ ^ & | \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.OP_BITS_SHIFT_LEFT, TokenType.OP_BITS_SHIFT_RIGHT, TokenType.OP_BITS_COMPLEMENT, TokenType.OP_BITS_XOR, TokenType.OP_BITS_AND, TokenType.OP_BITS_OR};

            var current = lexer.GetNextToken().type;
            var i = 0;
            while(current != TokenType.EOF && i < expectedTypes.Length){
                Assert.True(expectedTypes[i++] == current);
                current = lexer.GetNextToken().type;
            }

            Assert.True(i == expectedTypes.Length);
        }

        [Fact]
        public void IncrementOperators(){
            var input = new InputString("++ -- += \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.OP_INC_PP, TokenType.OP_INC_MM, TokenType.OP_INC_PE};

            var current = lexer.GetNextToken().type;
            var i = 0;
            while(current != TokenType.EOF && i < expectedTypes.Length){
                Assert.True(expectedTypes[i++] == current);
                current = lexer.GetNextToken().type;
            }

            Assert.True(i == expectedTypes.Length);
        }

        [Fact]
        public void MultipleOptionsOperators(){
            var input = new InputString("+ ++ +=  - -- = == ? ?? ! != | || & &&  \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.OP_SUM, TokenType.OP_INC_PP, TokenType.OP_INC_PE, TokenType.OP_SUBSTRACT, TokenType.OP_INC_MM,
                                                TokenType.OP_ASSIGN, TokenType.OP_CONDITIONAL_EQUAL, TokenType.OP_CONDITIONAL, TokenType.OP_NULL_COALESCING,
                                                TokenType.OP_NEGATION, TokenType.OP_CONDITIONAL_NOT_EQUAL, TokenType.OP_BITS_OR, TokenType.OP_CONDITIONAL_OR,
                                                TokenType.OP_BITS_AND, TokenType.OP_CONDITIONAL_AND};

            var current = lexer.GetNextToken().type;
            var i = 0;
            while(current != TokenType.EOF && i < expectedTypes.Length){
                Assert.True(expectedTypes[i++] == current);
                current = lexer.GetNextToken().type;
            }

            Assert.True(i == expectedTypes.Length);
        }

        [Fact]
        public void AsIsOperators(){
            var input = new InputString("as is \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.OP_AS, TokenType.OP_IS};

            var current = lexer.GetNextToken().type;
            var i = 0;
            while(current != TokenType.EOF && i < expectedTypes.Length){
                Assert.True(expectedTypes[i++] == current);
                current = lexer.GetNextToken().type;
            }

            Assert.True(i == expectedTypes.Length);
        }

        [Fact]
        public void Symbols(){
            var input = new InputString("() {} [] : . \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.PAREN_OPEN, TokenType.PAREN_CLOSE, TokenType.BRACE_OPEN, TokenType.BRACE_CLOSE, TokenType.BRACKET_OPEN,
                                                TokenType.BRACKET_CLOSE, TokenType.OP_HIERARCHY, TokenType.OP_MEMBER_ACCESS};

            var current = lexer.GetNextToken().type;
            var i = 0;
            while(current != TokenType.EOF && i < expectedTypes.Length){
                Assert.True(expectedTypes[i++] == current);
                current = lexer.GetNextToken().type;
            }

            Assert.True(i == expectedTypes.Length);
        }
    }
}
