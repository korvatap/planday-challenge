using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using CarFactory.Mappers;
using CarFactory.Models;
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
        public async Task<object> Post([FromBody] [Required] BuildCarInputModel carsSpecs)
        {
            var wantedCars = _carSpecificationMapper.Map(carsSpecs);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var cars = await _carFactory.BuildCars(wantedCars);
            stopwatch.Stop();
            
            Console.WriteLine($"Elapsed {stopwatch.ElapsedMilliseconds}");

            return new BuildCarOutputModel(stopwatch.ElapsedMilliseconds, cars);
        }
    }
}