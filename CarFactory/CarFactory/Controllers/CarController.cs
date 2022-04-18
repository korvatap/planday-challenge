using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CarFactory.Mappers;
using CarFactory.Models;
using CarFactory_Domain;
using CarFactory_Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarFactory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarFactory _carFactory;
        private readonly ICarSpecificationMapper _carSpecificationMapper;

        public CarController(ICarFactory carFactory, ICarSpecificationMapper carSpecificationMapper)
        {
            _carFactory = carFactory;
            _carSpecificationMapper = carSpecificationMapper;
        }

        [ProducesResponseType(typeof(BuildCarOutputModel), StatusCodes.Status200OK)]
        [HttpPost]
        public object Post([FromBody] [Required] BuildCarInputModel carsSpecs)
        {
            var wantedCars = _carSpecificationMapper.Map(carsSpecs);
            //Build cars
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var cars = _carFactory.BuildCars(wantedCars);
            stopwatch.Stop();

            //Create response and return
            return new BuildCarOutputModel
            {
                Cars = cars,
                RunTime = stopwatch.ElapsedMilliseconds
            };
        }
    }
}