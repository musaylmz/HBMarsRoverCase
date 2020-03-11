namespace HBMarsRover.Common.Exceptions
{
    public class InvalidMovementAbilityParamException : BadRequestException
    {
        public InvalidMovementAbilityParamException()
        {

        }

        public InvalidMovementAbilityParamException(string message) : base(message)
        {

        }
    }
}
