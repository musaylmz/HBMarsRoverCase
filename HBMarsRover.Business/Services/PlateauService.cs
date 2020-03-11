using HBMarsRover.Business.Interfaces;
using HBMarsRover.Common.Exceptions;
using HBMarsRover.Model;

namespace HBMarsRover.Business.Services
{
    public class PlateauService : IPlateauService
    {
        #region |   METHOD's   |

        public PlateauModel DrawPlateau(PlateauModel model)
        {
            if (model.Width == 0 || model.Height == 0)
            {
                throw new InvalidParameterOfPlateauException("Invalid Parameters For Drawing Plataeu Area");
            }

            return model;
        }

        #endregion
    }
}
