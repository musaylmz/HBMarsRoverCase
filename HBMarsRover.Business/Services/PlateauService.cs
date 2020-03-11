using HBMarsRover.Business.Interfaces;
using HBMarsRover.Common.Exceptions;
using HBMarsRover.Model;

namespace HBMarsRover.Business.Services
{
    public class PlateauService : IPlateauService
    {
        #region |   METHOD's   |

        /// <summary>
        /// Platonun oluşturulmasını sağlar.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public PlateauModel DrawPlateau(PlateauModel model)
        {
            if (model.Width == 0 || model.Height == 0)
            {
                throw new InvalidParameterOfPlateauException("Invalid parameters for drawing plataeu area");
            }

            return model;
        }

        #endregion
    }
}
