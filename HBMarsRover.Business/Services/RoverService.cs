using HBMarsRover.Business.Interfaces;
using HBMarsRover.Common.Enums;
using HBMarsRover.Common.Exceptions;
using HBMarsRover.Model;
using System;

namespace HBMarsRover.Business.Services
{
    public class RoverService : IRoverService
    {
        #region |   METHOD's   |

        public RoverModel SetRoverOnPlateau(PlateauModel plateau, DeploymentPointModel deploymentPoint)
        {
            CheckDeploymentPoint(plateau, deploymentPoint);
            CheckDirection(deploymentPoint.Direction);
            return new RoverModel() { DeploymentPoint = deploymentPoint };
        }

        public RoverModel CalculateRoverMovement(RoverModel rover, PlateauModel plateau)
        {
            if (rover == null)
                throw new BadRequestException("Please Check your requested Rover Model.");
            return Move(rover, plateau);
        }

        #endregion

        #region |   PRIVATE METHOD's   |

        private void CheckDirection(Direction direction)
        {
            Enum.TryParse(typeof(Direction), direction.ToString(), out var result);
            if (result == null)
                throw new InvalidDirectionException("Invalid Direction. Please Check your requested model");
        }

        private void CheckDeploymentPoint(PlateauModel plateau, DeploymentPointModel deploymentPoint)
        {
            if (plateau.Width < deploymentPoint.X || plateau.Height < deploymentPoint.Y || deploymentPoint.X < 0 || deploymentPoint.Y < 0)
                throw new OutOfBoundsFromPlateauException("Rover can't located on plateau.");
        }

        private RoverModel Move(RoverModel rover, PlateauModel plateau)
        {
            foreach (var movingDirection in rover.Movement.MovementList)
            {
                switch (movingDirection)
                {
                    case MovementAbility.L:
                        MoveLeft(rover);
                        break;
                    case MovementAbility.R:
                        MoveRight(rover);
                        break;
                    case MovementAbility.M:
                        MoveStraight(rover, plateau);
                        break;
                    default:
                        throw new InvalidMovementAbilityParamException("Invalid Moving Key For Rover");
                }
            }

            return rover;
        }

        private void MoveStraight(RoverModel rover, PlateauModel plateau)
        {
            switch (rover.DeploymentPoint.Direction)
            {
                case Direction.N:
                    if (rover.DeploymentPoint.Y + 1 <= plateau.Height)
                        rover.DeploymentPoint.Y += 1;
                    break;

                case Direction.E:
                    if (rover.DeploymentPoint.X + 1 <= plateau.Width)
                        rover.DeploymentPoint.X += 1;
                    break;

                case Direction.S:
                    if (rover.DeploymentPoint.Y - 1 <= plateau.Height)
                        rover.DeploymentPoint.Y -= 1;
                    break;

                case Direction.W:
                    if (rover.DeploymentPoint.X - 1 <= plateau.Width)
                        rover.DeploymentPoint.X -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void MoveRight(RoverModel rover)
        {
            rover.DeploymentPoint.Direction = rover.DeploymentPoint.Direction switch
            {
                Direction.N => Direction.E,
                Direction.E => Direction.S,
                Direction.S => Direction.W,
                Direction.W => Direction.N,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private void MoveLeft(RoverModel rover)
        {
            rover.DeploymentPoint.Direction = rover.DeploymentPoint.Direction switch
            {
                Direction.N => Direction.W,
                Direction.W => Direction.S,
                Direction.S => Direction.E,
                Direction.E => Direction.N,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        #endregion
    }
}
