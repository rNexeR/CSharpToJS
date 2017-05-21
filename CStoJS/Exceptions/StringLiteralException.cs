using System;

namespace CStoJS.Exceptions
{
    public class StringLiteralException : Exception
    {
        public StringLiteralException()
        {
        }

        public StringLiteralException(string message) : base(message)
        {
        }

        public StringLiteralException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}