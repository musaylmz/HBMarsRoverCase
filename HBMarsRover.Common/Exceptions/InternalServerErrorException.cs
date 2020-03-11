using System;

namespace HBMarsRover.Common.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException()
        {
        }

        public InternalServerErrorException(string message) : base(message)
        {
        }

        public InternalServerErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
