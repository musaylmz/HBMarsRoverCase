using HBMarsRover.Model;

namespace HBMarsRover.Business.Interfaces
{
    public interface IRoverService
    {
        RoverModel SetRoverOnPlateau(PlateauModel plateau, DeploymentPointModel deploymentPoint);

        RoverModel CalculateRoverMovement(RoverModel rover, PlateauModel plateau);
    }
}
