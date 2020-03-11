namespace HBMarsRover.Common.Exceptions
{
    public class InvalidDirectionException : BadRequestException
    {
        public InvalidDirectionException()
        {

        }

        public InvalidDirectionException(string message) : base(message)
        {

        }
    }
}
