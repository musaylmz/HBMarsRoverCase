using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HBMarsRover.API.Models;
using HBMarsRover.Business.Interfaces;
using HBMarsRover.Common.Enums;
using HBMarsRover.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HBMarsRover.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarsRoverController : BaseController
    {
        private readonly ILogger<MarsRoverController> _logger;
        private readonly IPlateauService _plateauService;
        private readonly IRoverService _roverService;

        public MarsRoverController(ILogger<MarsRoverController> logger, IPlateauService plateauService, IRoverService roverService) : base(logger)
        {
            _logger = logger;
            _plateauService = plateauService;
            _roverService = roverService;
        }

        [HttpGet]
        public object GetTest()
        {
            return string.Empty;
        }


        [HttpPost]
        public object Post([FromBody] MarsRoverModel model)
        {
            var roverList = new List<RoverModel>();
            foreach (var roverItem in model.Rovers)
            {
                var plateau = _plateauService.DrawPlateau(model.Plateau);
                var rover = _roverService.SetRoverOnPlateau(model.Plateau, new DeploymentPointModel(roverItem.DeploymentPoint.Direction.ToString())
                {
                    X = roverItem.DeploymentPoint.X,
                    Y = roverItem.DeploymentPoint.Y
                });
                var movements = roverItem.Movement.ToString()
                    .ToCharArray()
                      .Select(x => Enum.Parse<MovementAbility>(x.ToString()))
                          .ToList();

                rover.Movement.MovementList = movements;
                roverList.Add(_roverService.CalculateRoverMovement(rover, plateau));
            }
            return roverList.Select(x => new DeploymentPointModel(x.DeploymentPoint.Direction.ToString())
            {
                X = x.DeploymentPoint.X,
                Y = x.DeploymentPoint.Y,
                Direction = x.DeploymentPoint.Direction
            });
        }
    }
}