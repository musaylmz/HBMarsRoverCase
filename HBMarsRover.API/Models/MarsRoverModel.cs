using HBMarsRover.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HBMarsRover.API.Models
{
    public class MarsRoverModel
    {
        public PlateauModel Plateau { get; set; }
        public List<RoverModel> Rovers { get; set; }
    }
}
