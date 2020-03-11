namespace HBMarsRover.Common.Exceptions
{
    public class InvalidParameterOfPlateauException : BadRequestException
    {
        public InvalidParameterOfPlateauException()
        {

        }

        public InvalidParameterOfPlateauException(string message) : base(message)
        {

        }
    }
}
