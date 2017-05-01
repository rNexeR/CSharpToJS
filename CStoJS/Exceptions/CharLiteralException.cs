using System;

namespace CStoJS
{
    public class CharLiteralException : Exception
    {
        public CharLiteralException()
        {
        }

        public CharLiteralException(string message) : base(message)
        {
        }

        public CharLiteralException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}