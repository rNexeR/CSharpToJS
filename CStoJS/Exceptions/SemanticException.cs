using System;
using CStoJS.LexerLibraries;

namespace CStoJS.Exceptions
{
    public class SemanticException : Exception
    {

        public SemanticException()
        {
        }

        public SemanticException(string message) : base(message)
        {
        }

        public SemanticException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public SemanticException(string message, Token token) : this($"{message} Col: {token.column} Row: {token.row}")
        {
        }
    }
}