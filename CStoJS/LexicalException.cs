using System;

namespace CStoJS
{
    public class LexicalException : Exception
    {
        public LexicalException()
        {
        }

        public LexicalException(string message) : base(message)
        {
        }

        public LexicalException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}