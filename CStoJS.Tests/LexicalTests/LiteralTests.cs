using System;
using Xunit;
using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;

namespace CStoJS.Tests
{
    public class LiteralTests
    {
        [Fact]
        public void CharLiteralMustBeClosed(){
            var input = new InputString("'a' 'b' ' \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.LITERAL_CHAR, TokenType.LITERAL_CHAR};
            var expectedLexemas = new string[]{"'a'", "'b'"};

            var current = lexer.GetNextToken();
            var i = 0;
            while(current.type != TokenType.EOF && i < expectedTypes.Length-1){
                Assert.Equal(expectedTypes[i] , current.type);
                Assert.Equal(expectedLexemas[i++] , current.lexema);
                current = lexer.GetNextToken();
            }

            // Exception ex = Assert.Throws<CharLiteralException>(() => lexer.GetNextToken());

            // Assert.Equal("Char Literal must be closed.", ex.Message);
        }

        [Fact]
        public void CharLiteralTooManyCharacters(){
            var input = new InputString("'a' 'b' 'as' \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.LITERAL_CHAR, TokenType.LITERAL_CHAR};
            var expectedLexemas = new string[]{"'a'", "'b'"};

            var current = lexer.GetNextToken();
            var i = 0;
            while(current.type != TokenType.EOF && i < expectedTypes.Length-1){
                Assert.Equal(expectedTypes[i] , current.type);
                Assert.Equal(expectedLexemas[i++] , current.lexema);
                current = lexer.GetNextToken();
            }

            // Exception ex = Assert.Throws<CharLiteralException>(() => lexer.GetNextToken());

            // Assert.Equal("Too many characters in Char Literal.", ex.Message);
        }

        [Fact]
        public void CorrectCharLiteral(){
            var input = new InputString("&& 'a' 'b' '\t' \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.OP_CONDITIONAL_AND, TokenType.LITERAL_CHAR, TokenType.LITERAL_CHAR, TokenType.LITERAL_CHAR};
            var expectedLexemas = new string[]{"&&","'a'", "'b'", "'\t'"};

            var current = lexer.GetNextToken();
            var i = 0;
            while(current.type != TokenType.EOF && i < expectedTypes.Length-1){
                Assert.Equal(expectedTypes[i] , current.type);
                Assert.Equal(expectedLexemas[i++] , current.lexema);
                current = lexer.GetNextToken();
            }
        }

        [Fact]
        public void CorrectStringLiteral(){
            var input = new InputString("&& \"a\" \"break\" \"hello\" \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.OP_CONDITIONAL_AND, TokenType.LITERAL_STRING, TokenType.LITERAL_STRING, TokenType.LITERAL_STRING};
            var expectedLexemas = new string[]{"&&","\"a\"", "\"break\"", "\"hello\""};

            var current = lexer.GetNextToken();
            var i = 0;
            while(current.type != TokenType.EOF && i < expectedTypes.Length-1){
                Assert.Equal(expectedTypes[i] , current.type);
                Assert.Equal(expectedLexemas[i++] , current.lexema);
                current = lexer.GetNextToken();
            }
        }

        [Fact]
        public void CorrectStringVerbatimLiteral(){
            var input = new InputString("&& \"a\" \"break\" \"hello\" @\"hi\" \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.OP_CONDITIONAL_AND, TokenType.LITERAL_STRING, TokenType.LITERAL_STRING, TokenType.LITERAL_STRING, TokenType.LITERAL_STRING_VERBATIM};
            var expectedLexemas = new string[]{"&&","\"a\"", "\"break\"", "\"hello\"", "\"hi\""};

            var current = lexer.GetNextToken();
            var i = 0;
            while(current.type != TokenType.EOF && i < expectedTypes.Length-1){
                Assert.Equal(expectedTypes[i] , current.type);
                Assert.Equal(expectedLexemas[i++] , current.lexema);
                current = lexer.GetNextToken();
            }
        }

        [Fact]
        public void CorrectIntLiteral(){
            var input = new InputString("1 23 5 \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.LITERAL_INT, TokenType.LITERAL_INT, TokenType.LITERAL_INT};
            var expectedLexemas = new string[]{"1","23", "5"};

            var current = lexer.GetNextToken();
            var i = 0;
            while(current.type != TokenType.EOF && i < expectedTypes.Length-1){
                Assert.Equal(expectedTypes[i], current.type);
                Assert.Equal(expectedLexemas[i++], current.lexema);
                current = lexer.GetNextToken();
            }
        }

        [Fact]
        public void CorrectFloatLiteral(){
            var input = new InputString("1.5F 23.7F 5.0F \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.LITERAL_FLOAT, TokenType.LITERAL_FLOAT, TokenType.LITERAL_FLOAT};
            var expectedLexemas = new string[]{"1.5F","23.7F", "5.0F"};

            var current = lexer.GetNextToken();
            var i = 0;
            while(current.type != TokenType.EOF && i < expectedTypes.Length-1){
                Assert.Equal(expectedTypes[i], current.type);
                Assert.Equal(expectedLexemas[i++], current.lexema);
                current = lexer.GetNextToken();
            }
        }

        [Fact]
        public void FloatLiteralMustFinishWithF(){
            var input = new InputString("1.5F 23.7F 5.0 5.0.0 \0");
            var lexer = new Lexer(input);

            var expectedTypes = new TokenType[]{TokenType.LITERAL_FLOAT, TokenType.LITERAL_FLOAT};
            var expectedLexemas = new string[]{"1.5F","23.7F"};

            var current = lexer.GetNextToken();
            var i = 0;
            while(current.type != TokenType.EOF && i < expectedTypes.Length-1){
                Assert.Equal(expectedTypes[i], current.type);
                Assert.Equal(expectedLexemas[i++], current.lexema);
                current = lexer.GetNextToken();
            }

            // Exception ex = Assert.Throws<FloatLiteralException>(() => lexer.GetNextToken());

            // Assert.Equal("Float Literal must finish with F.", ex.Message);

            // ex = Assert.Throws<FloatLiteralException>(() => lexer.GetNextToken());

            // Assert.Equal("Float Literal must finish with F.", ex.Message);
        }
    }
}