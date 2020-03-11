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

        /// <summary>
        /// Gezicinin plato üzerine konumlanmasını sağlar.
        /// </summary>
        /// <param name="plateau"></param>
        /// <param name="deploymentPoint"></param>
        /// <returns></returns>
        public RoverModel SetRoverOnPlateau(PlateauModel plateau, DeploymentPointModel deploymentPoint)
        {
            CheckDeploymentPoint(plateau, deploymentPoint);
            CheckDirection(deploymentPoint.Direction);
            return new RoverModel() { DeploymentPoint = deploymentPoint };
        }

        /// <summary>
        /// Gezicinin plato üzerinde hareketini hesaplar.
        /// </summary>
        /// <param name="rover"></param>
        /// <param name="plateau"></param>
        /// <returns></returns>
        public RoverModel CalculateRoverMovement(RoverModel rover, PlateauModel plateau)
        {
            if (rover == null)
                throw new BadRequestException("Please check your requested Rover Model.");
            return Move(rover, plateau);
        }

        #endregion

        #region |   PRIVATE METHOD's   |

        /// <summary>
        /// Yön kontrolü
        /// </summary>
        /// <param name="direction"></param>
        private void CheckDirection(Direction direction)
        {
            Enum.TryParse(typeof(Direction), direction.ToString(), out var result);
            if (result == null)
                throw new InvalidDirectionException("Invalid direction. Please check your requested model");
        }

        /// <summary>
        /// Konumlanma koordinatlarının platoya göre kontrolü
        /// </summary>
        /// <param name="plateau"></param>
        /// <param name="deploymentPoint"></param>
        private void CheckDeploymentPoint(PlateauModel plateau, DeploymentPointModel deploymentPoint)
        {
            if (plateau.Width < deploymentPoint.X || plateau.Height < deploymentPoint.Y || deploymentPoint.X < 0 || deploymentPoint.Y < 0)
                throw new OutOfBoundsFromPlateauException("Rover can't located on plateau.");
        }

        /// <summary>
        /// İstenilen yönüne göre hareket eylemine yönlendirir.
        /// </summary>
        /// <param name="rover"></param>
        /// <param name="plateau"></param>
        /// <returns></returns>
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
                        throw new InvalidMovementAbilityParamException("Invalid moving key for rover");
                }
            }

            return rover;
        }

        /// <summary>
        /// Düz ileriye yapılan hareketi sağlar.
        /// </summary>
        /// <param name="rover"></param>
        /// <param name="plateau"></param>
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

        /// <summary>
        /// Sağa hareket eylemini sağlar.
        /// </summary>
        /// <param name="rover"></param>
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

        /// <summary>
        /// Sola hareket eylemini sağlar.
        /// </summary>
        /// <param name="rover"></param>
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
