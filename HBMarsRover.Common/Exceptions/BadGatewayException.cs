using System;

namespace HBMarsRover.Common.Exceptions
{
    public class BadGatewayException : BaseException
    {
        public BadGatewayException()
        {
        }

        public BadGatewayException(string message) : base(message)
        {
        }

        public BadGatewayException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
