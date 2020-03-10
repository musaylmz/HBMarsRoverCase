using HBMarsRover.Business.Services;
using HBMarsRover.Common.Enums;
using HBMarsRover.Model;
using System;
using System.Linq;
using Xunit;

namespace HBMarsRover.Business.Test
{
    public class RoverTest
    {
        private readonly PlateauService _plateauService;
        private readonly RoverService _roverService;

        public RoverTest()
        {
            _plateauService = new PlateauService();
            _roverService = new RoverService();
        }

        [Theory]
        [InlineData(2, 4, "N")]
        [InlineData(3, 3, "S")]
        [InlineData(5, 6, "W")]
        [InlineData(8, 2, "E")]
        public void SetRoverOnPlateau_Success_CorrectParams(int expectedX, int expectedY, string expectedDirection)
        {
            // Arrange
            var plataue = new PlateauModel(8, 6);

            // Act
            var rover = _roverService.SetRoverOnPlateau(plataue, new DeploymentPointModel(expectedDirection)
            {
                X = expectedX,
                Y = expectedY
            });

            // Assert
            Assert.Equal(expectedX, rover.DeploymentPoint.X);
            Assert.Equal(expectedY, rover.DeploymentPoint.Y);
            Assert.Equal(Enum.Parse<Direction>(expectedDirection), rover.DeploymentPoint.Direction);
        }

        [Theory]
        [InlineData(6, 4, "E")]
        [InlineData(2, 5, "W")]
        [InlineData(1, 5, "N")]
        [InlineData(1, 5, "S")]
        public void SetRoverOnPlateau_OutOfBoundsFromPlateauException_WhenHigherPointDirectionParams(int expectedX, int expectedY, string expectedDirection)
        {
            // Arrange
            var plataue = new PlateauModel(2, 4);

            // Act
            var action = new Action(() => _roverService.SetRoverOnPlateau(plataue, new DeploymentPointModel(expectedDirection)
            {
                X = expectedX,
                Y = expectedY
            }));

            // Assert
            Assert.Throws<Exception>(action);
        }

        [Theory]
        [InlineData(6, 4, "B")]
        [InlineData(2, 5, "Y")]
        [InlineData(1, 5, "R")]
        [InlineData(1, 5, "Q")]
        public void SetRoverOnPlateau_InvalidDirectionException_WhenDirectionIsInvalid(int expectedX, int expectedY, string expectedDirection)
        {
            // Arrange
            var plataue = new PlateauModel(10, 10);

            // Act
            var action = new Action(() => _roverService.SetRoverOnPlateau(plataue, new DeploymentPointModel(expectedDirection)
            {
                X = expectedX,
                Y = expectedY
            }));

            // Assert
            Assert.Throws<Exception>(action);
        }

        [Theory]
        [InlineData("MM")]
        [InlineData("MMLR")]
        [InlineData("LLLLMM")]
        public void CalculateRoverMovement_Success_IsAcceptableMovementList(string calculationCommand)
        {
            // Arrange
            var plataue = new PlateauModel(5, 5);
            //Act
            var rover = _roverService.SetRoverOnPlateau(plataue, new DeploymentPointModel(Direction.N.ToString())
            {
                X = 3,
                Y = 3
            });
            var movements = calculationCommand
                      .ToCharArray()
                      .Select(x => Enum.Parse<MovementAbility>(x.ToString()))
                      .ToList();

            rover.Movement.MovementList = movements;

            //Assert
            Assert.NotNull(plataue);
            Assert.NotNull(rover.Movement.MovementList);
            Assert.Equal(3, rover.DeploymentPoint.X);
            Assert.Equal(3, rover.DeploymentPoint.Y);
        }

        [Theory]
        [InlineData("LMLMLMLMM")]
        public void CalculateRoverMovement_Success_TaskInput1(string calculationCommand)
        {
            // Arrange
            var plataue = new PlateauModel(5, 5);
            //Act
            var rover = _roverService.SetRoverOnPlateau(plataue, new DeploymentPointModel(Direction.N.ToString())
            {
                X = 1,
                Y = 2
            });
            var movements = calculationCommand
                      .ToCharArray()
                  .Select(x => Enum.Parse<MovementAbility>(x.ToString()))
                      .ToList();

            rover.Movement.MovementList = movements;
            _roverService.CalculateRoverMovement(rover, plataue);

            //Assert
            Assert.NotNull(plataue);
            Assert.NotNull(rover.Movement.MovementList);
            Assert.Equal(1, rover.DeploymentPoint.X);
            Assert.Equal(3, rover.DeploymentPoint.Y);
            Assert.Equal(Direction.N, rover.DeploymentPoint.Direction);
        }

        [Theory]
        [InlineData("MMRMMRMRRM")]
        public void CalculateRoverMovement_Success_TaskInput2(string calculationCommand)
        {
            // Arrange
            var plataue = new PlateauModel(5, 5);
            //Act
            var rover = _roverService.SetRoverOnPlateau(plataue, new DeploymentPointModel(Direction.E.ToString())
            {
                X = 3,
                Y = 3
            });
            var movements = calculationCommand
                      .ToCharArray()
                  .Select(x => Enum.Parse<MovementAbility>(x.ToString()))
                      .ToList();

            rover.Movement.MovementList = movements;
            _roverService.CalculateRoverMovement(rover, plataue);

            //Assert
            Assert.NotNull(plataue);
            Assert.NotNull(rover.Movement.MovementList);
            Assert.Equal(5, rover.DeploymentPoint.X);
            Assert.Equal(1, rover.DeploymentPoint.Y);
            Assert.Equal(Direction.E, rover.DeploymentPoint.Direction);
        }
    }
}
