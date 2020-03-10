namespace HBMarsRover.Model
{
    public class RoverModel : BaseModel, IRover
    {
        public RoverModel()
        {
            Movement = new MovementModel();
        }

        public DeploymentPointModel DeploymentPoint { get; set; }
        public MovementModel Movement { get; set; }
    }
}
