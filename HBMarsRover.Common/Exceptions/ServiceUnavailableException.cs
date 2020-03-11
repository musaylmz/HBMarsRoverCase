using System;

namespace HBMarsRover.Common.Exceptions
{
    public class ServiceUnavailableException : BaseException
    {
        public ServiceUnavailableException()
        {
        }

        public ServiceUnavailableException(string message) : base(message)
        {
        }

        public ServiceUnavailableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
