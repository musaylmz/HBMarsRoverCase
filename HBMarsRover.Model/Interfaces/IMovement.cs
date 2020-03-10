using System;
using System.Collections.Generic;
using System.Text;
using HBMarsRover.Common.Enums;

namespace HBMarsRover.Model
{
    public interface IMovement
    {
        List<MovementAbility> MovementList { get; set; }
    }
}
