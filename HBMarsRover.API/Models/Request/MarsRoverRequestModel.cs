using HBMarsRover.Model;
using System.Collections.Generic;

namespace HBMarsRover.API.Models
{
    public class MarsRoverRequestModel
    {
        public PlateauModel Plateau { get; set; }
        public List<RoverRequestModel> Rovers { get; set; }
    }
}
