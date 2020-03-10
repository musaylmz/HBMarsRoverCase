using HBMarsRover.Common.Enums;
using System.Collections.Generic;

namespace HBMarsRover.Model
{
    public class MovementModel : BaseModel, IMovement
    {
        public List<MovementAbility> MovementList { get; set; }
    }
}
