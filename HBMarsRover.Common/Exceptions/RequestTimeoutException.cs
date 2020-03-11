using System;

namespace HBMarsRover.Common.Exceptions
{
    public class RequestTimeoutException : BaseException
    {
        public RequestTimeoutException()
        {
        }

        public RequestTimeoutException(string message) : base(message)
        {
        }

        public RequestTimeoutException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
