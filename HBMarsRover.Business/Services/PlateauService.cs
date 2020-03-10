using HBMarsRover.Business.Interfaces;
using HBMarsRover.Model;
using System;

namespace HBMarsRover.Business.Services
{
    public class PlateauService : IPlateauService
    {
        #region |   METHOD's   |

        public PlateauModel DrawPlateau(PlateauModel model)
        {
            if (model.Width == 0 || model.Height == 0)
            {
                throw new Exception("Invalid Parameters For Drawing Plataeu Area");
            }

            return model;
        }

        #endregion
    }
}
