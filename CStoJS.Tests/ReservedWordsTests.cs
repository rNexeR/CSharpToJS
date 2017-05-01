using System;
using Xunit;

namespace CStoJS.Tests
{
    public class ReservedWordsTests
    {
        [Fact]
        public void ReservedWords(){
            var input = new InputString("int float bool string char true false as is \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.INT_TYPE, TokenType.FLOAT_TYPE, TokenType.BOOL_TYPE, TokenType.STRING_TYPE, TokenType.CHAR_TYPE, 
                                                TokenType.TRUE_BOOL_VALUE, TokenType.FALSE_BOOL_VALUE, TokenType.OP_AS, TokenType.OP_IS};

            var current = lexer.GetNextToken();
            var i = 0;
            while(current.type != TokenType.EOF && i < expectedTypes.Length){
                Assert.Equal(expectedTypes[i++], current.type);
                current = lexer.GetNextToken();
            }

            Assert.Equal(i, expectedTypes.Length);
        }
    }
}