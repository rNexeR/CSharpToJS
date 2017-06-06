using CStoJS.LexerLibraries;
using CStoJS.Inputs;
using CStoJS.Exceptions;
using System.Linq;
using System;
using System.Collections.Generic;
using CStoJS.Tree;

namespace CStoJS.ParserLibraries
{
    public partial class Parser
    {
        public void ThrowSyntaxException(string msg)
        {
            throw new SyntaxException(msg + $" {currentToken.type} provided" + ". [" + currentToken.row + "," + currentToken.column + "]");
        }

        public bool Match(TokenType expected)
        {
            return expected == currentToken.type;
        }

        public bool ConsumeOnMatch(TokenType expected)
        {
            if (expected == currentToken.type)
            {
                ConsumeToken();
                // Console.WriteLine($"\tCOM {expected} == CT {currentToken.type}");
                return true;
            }
            return false;
        }

        public Token MatchExactly(TokenType expected)
        {
            if (expected == currentToken.type)
            {
                var ret = currentToken;
                ConsumeToken();
                return ret;
            }
            ThrowSyntaxException($"{expected} expected");
            return null;
        }

        public Token MatchOne(TokenType[] expectedTokens, string msg)
        {
            foreach (var expected in expectedTokens)
            {
                if (currentToken.type == expected)
                {
                    var ret = currentToken;
                    ConsumeToken();
                    return ret;
                }

            }
            ThrowSyntaxException(msg);
            return null;
        }

        public bool MatchAny(TokenType[] expectedList)
        {
            return expectedList.Contains(currentToken.type);
        }

        // public bool MatchAndComsumeAny( TokenType[] expectedList){
        // 	if( expectedList.Contains(currentToken.type) ){
        //         ConsumeToken();
        //         return true;
        //     }
        //     return false;
        // }

        public List<Token> MatchExactly(TokenType[] expectedTokens)
        {
            var ret = new List<Token>();
            foreach (var expected in expectedTokens)
            {
                if (currentToken.type != expected)
                {
                    ThrowSyntaxException($"{expected} expected");
                }
                ret.Add(currentToken);
                ConsumeToken();
            }
            return ret;
        }

        // public bool OptionalMatchExactly(TokenType[] expectedTokens){
        //     foreach(var expected in expectedTokens){
        //         if(currentToken.type != expected){
        //             if(expected == expectedTokens[0])
        //                 return false;
        //             ThrowSyntaxException($"{expected} expected");
        //         }
        //         ConsumeToken();
        //     }
        //     return true;
        // }

        public bool ConsumeOnMatchLA(TokenType expected)
        {
            var tok = currentToken;
            if (Match(expected))
            {
                ConsumeToken();
                this.lookAhead = this.lookAhead.Concat(new Token[] { tok }).ToArray();
                return true;
            }
            return false;
        }

        public bool MatchAndComsumeAnyLA(TokenType[] expectedList)
        {
            var tok = currentToken;
            if (MatchAny(expectedList))
            {
                ConsumeToken();
                this.lookAhead = this.lookAhead.Concat(new Token[] { tok }).ToArray();
                return true;
            }
            return false;
        }

        public void printDebug(string msg)
        {
            if (enableDebug)
            {
                Console.WriteLine(msg + " at " + currentToken.row + "," + currentToken.column + $". [{currentToken.type} = {currentToken.lexema}]");
                // if(!(lookAhead.Length > 0))
                //     Console.WriteLine(msg + " at " + currentToken.row + "," + currentToken.column + $". [{currentToken.type} = {currentToken.lexema}] [LA <{lookAhead.Length}> ]");
                // else
                //     Console.WriteLine(msg + " at " + currentToken.row + "," + currentToken.column + $". [{currentToken.type} = {currentToken.lexema}] [LA <{lookAhead.Length}> <{lookAhead[0]}> ]");
            }
        }

        private Token ConsumeToken()
        {
            var token = currentToken;
            if (lookAheadBack && lookAhead.Length > 0)
            {
                currentToken = lookAhead[0];
                var temp = lookAhead.ToList();
                temp.RemoveAt(0);
                lookAhead = temp.ToArray();
                if (lookAhead.Length == 0)
                    lookAheadBack = false;
            }
            else
            {
                currentToken = lexer.GetNextToken();
            }
            return token;
        }

        private void RollbackLA()
        {
            if (lookAhead.Length == 0)
                return;
            lookAheadBack = true;
            var temp = lookAhead.ToList();
            temp.Add(currentToken);
            lookAhead = temp.ToArray();
            ConsumeToken();
        }

        private TypeDeclarationNode TypeDetector(TokenType token, IdentifierNode identifier)
        {

            switch (token)
            {
                case TokenType.INT_KEYWORD: return new IntType(identifier);
                case TokenType.CHAR_KEYWORD: return new CharType(identifier);
                case TokenType.STRING_KEYWORD: return new StringType(identifier);
                case TokenType.FLOAT_KEYWORD: return new FloatType(identifier);
                case TokenType.VOID_KEYWORD: return new VoidType(identifier);
                case TokenType.BOOL_KEYWORD: return new BoolType(identifier);
                default: return new IdentifierTypeNode(identifier);
            }
        }

        private BinaryExpressionNode BinaryNodeDetector(ExpressionNode left, Token operador, ExpressionNode right){

            switch(operador.type){
                case TokenType.OP_NULL_COALESCING: return new NullCoalescingExpressionNode(left, operador, right);
                case TokenType.OP_CONDITIONAL_OR: return new ConditionalOrExpressionNode(left, operador, right);
                case TokenType.OP_CONDITIONAL_AND: return new ConditionalAndExpressionNode(left, operador, right);
                case TokenType.OP_CONDITIONAL_EQUAL: return new ConditionalEqualExpressionNode(left, operador, right);
                case TokenType.OP_CONDITIONAL_NOT_EQUAL: return new ConditionalNotEqualExpressionNode(left, operador, right);
                
                case TokenType.OP_BITS_OR: return new BitwiseOrExpressionNode(left, operador, right);
                case TokenType.OP_BITS_XOR: return new BitwiseXorExpressionNode(left, operador, right);
                case TokenType.OP_BITS_AND: return new BitwiseAndExpressionNode(left, operador, right);
                case TokenType.OP_BITS_SHIFT_LEFT: return new BitwiseShiftLeftExpressionNode(left, operador, right);
                case TokenType.OP_BITS_SHIFT_RIGHT: return new BitwiseShiftRightExpressionNode(left, operador, right);

                case TokenType.OP_SUM: return new ArithmeticSumExpressionNode(left, operador, right);
                case TokenType.OP_SUBSTRACT: return new ArithmeticSubstractExpressionNode(left, operador, right);
                case TokenType.OP_DIVISION: return new ArithmeticDivisionExpressionNode(left, operador, right);
                case TokenType.OP_MULTIPLICATION: return new ArithmeticMultiplicationExpressionNode(left, operador, right);
                case TokenType.OP_MODULO: return new ArithmeticModuloExpressionNode(left, operador, right);
                default: throw new SemanticException($"Cannot detect the Binary Expression that correspond to the operator {operador.lexema}");
            }
        }
    }
}