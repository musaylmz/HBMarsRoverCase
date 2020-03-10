using HBMarsRover.Common.Enums;

namespace HBMarsRover.Model
{
    public interface IDeploymentPoint
    {
        int X { get; set; }
        int Y { get; set; }
        string NavigatedDirection { get; }
        Direction Direction { get; }
        Direction SetDirection(string navigatedDirection);
    }
}
