using System;
using Xunit;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;

namespace CStoJS.Tests
{
    public class ReservedWordsTests
    {
        [Fact]
        public void ReservedWords(){
            var input = new InputString("int float bool string char true false as is \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.INT_KEYWORD, TokenType.FLOAT_KEYWORD, TokenType.BOOL_KEYWORD, TokenType.STRING_KEYWORD, TokenType.CHAR_KEYWORD, 
                                                TokenType.TRUE_KEYWORD, TokenType.FALSE_KEYWORD, TokenType.AS_KEYWORD, TokenType.IS_KEYWORD};

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