using HBMarsRover.Business.Interfaces;
using HBMarsRover.Model;
using System;

namespace HBMarsRover.Business.Services
{
    public class PlateauService : IPlateauService
    {
        public PlateauModel DrawPlateau(PlateauModel model)
        {
            if (model.Width == 0 || model.Height == 0)
            {
                throw new Exception("");
            }

            return model;
        }
    }
}
