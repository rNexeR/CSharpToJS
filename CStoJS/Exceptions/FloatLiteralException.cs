using System;

namespace CStoJS.Exceptions
{
    public class FloatLiteralException : Exception
    {
        public FloatLiteralException()
        {
        }

        public FloatLiteralException(string message) : base(message)
        {
        }

        public FloatLiteralException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}