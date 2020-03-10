using HBMarsRover.Common.Enums;
using System;

namespace HBMarsRover.Model
{
    public class DeploymentPointModel : BaseModel, IDeploymentPoint
    {
        public DeploymentPointModel(string navigatedDirection)
        {
            NavigatedDirection = navigatedDirection;
            SetDirection(NavigatedDirection);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public string NavigatedDirection { get; set; }
        public Direction Direction { get; set; }

        public Direction SetDirection(string navigatedDirection)
        {
            System.Enum.TryParse(typeof(Direction), navigatedDirection, out var result);
            if (result == null)
                throw new Exception("");

            return Direction = System.Enum.Parse<Direction>(Convert.ToString(result));
        }
    }
}
