using System;

namespace SecPortal.Commons.Exceptions
{
    public class CustomException : Exception
    {
        public int ErrorCode { get; set; }

        public CustomException()
        {

        }

        public CustomException(string message) : base(message)
        {

        }

        public CustomException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
