using HBMarsRover.Business.Services;
using HBMarsRover.Common.Exceptions;
using HBMarsRover.Model;
using System;
using Xunit;

namespace HBMarsRover.Business.Test
{
    public class PlateauTest
    {
        private readonly PlateauService _plateauService;

        public PlateauTest()
        {
            _plateauService = new PlateauService();
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(3, 3)]
        [InlineData(7, 2)]
        [InlineData(5, 9)]
        public void DrawPlateau_Success_WhenCorrectParams(int width, int height)
        {
            // Act
            var plataeu = new PlateauModel(width, height);

            //Assert
            Assert.Equal(plataeu.Width, width);
            Assert.Equal(plataeu.Height, height);
        }

        [Theory]
        [InlineData(0, 3)]
        [InlineData(7, 0)]
        [InlineData(0, 0)]
        [InlineData(15, 0)]
        public void DrawPlateau_InvalidRangeException_WhenPointsIsNull(int width, int height)
        {
            // Act
            var plataeu = new PlateauModel(width, height);
            var action = new Action(() => _plateauService.DrawPlateau(plataeu));

            // Assert
            Assert.Throws<InvalidParameterOfPlateauException>(action);
        }
    }
}
