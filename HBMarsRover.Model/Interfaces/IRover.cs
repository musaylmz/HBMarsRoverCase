namespace HBMarsRover.Model
{
    public interface IRover
    {
        DeploymentPointModel DeploymentPoint { get; set; }
        MovementModel Movement { get; set; }
    }
}
