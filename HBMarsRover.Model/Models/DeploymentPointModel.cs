using HBMarsRover.Common.Enums;
using HBMarsRover.Common.Exceptions;
using System;

namespace HBMarsRover.Model
{
    public class DeploymentPointModel : BaseModel, IDeploymentPoint
    {
        public DeploymentPointModel()
        {

        }

        public DeploymentPointModel(string navigatedDirection)
        {
            SetDirection(navigatedDirection);
        }

        public int X { get; set; }
        public int Y { get; set; }
        public Direction Direction { get; set; }
        public Direction SetDirection(string navigatedDirection)
        {
            System.Enum.TryParse(typeof(Direction), navigatedDirection, out var result);
            if (result == null)
                throw new InvalidDirectionException("Invalid Direction.Please Check your requested model");

            return Direction = System.Enum.Parse<Direction>(Convert.ToString(result));
        }
    }
}
