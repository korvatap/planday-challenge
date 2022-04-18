using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CarFactory.Controllers;
using CarFactory.Models;
using CarFactory_Domain;
using CarFactory_Factory;

namespace CarFactory.Mappers
{
    public interface ICarSpecificationMapper
    {
        IEnumerable<CarSpecification> Map(BuildCarInputModel carsSpecs);
    }

    public class CarSpecificationMapper : ICarSpecificationMapper
    {
        public IEnumerable<CarSpecification> Map(BuildCarInputModel carsSpecs)
        {
            //Check and transform specifications to domain objects
            var wantedCars = new List<CarSpecification>();
            foreach (var spec in carsSpecs.Cars)
            {
                for (var i = 1; i <= spec.Amount; i++)
                {
                    if (spec.Specification.NumberOfDoors % 2 == 0)
                    {
                        throw new ArgumentException("Must give an odd number of doors");
                    }

                    PaintJob? paint = null;
                    var baseColor = Color.FromName(spec.Specification.Paint.BaseColor);
                    switch (spec.Specification.Paint.Type.ToLower())
                    {
                        case "single":
                            paint = new SingleColorPaintJob(baseColor);
                            break;
                        case "stripe":
                            paint = new StripedPaintJob(baseColor,
                                Color.FromName(spec.Specification.Paint.StripeColor));
                            break;
                        case "dot":
                            paint = new DottedPaintJob(baseColor, Color.FromName(spec.Specification.Paint.DotColor));
                            break;
                        default:
                            throw new ArgumentException(string.Format("Unknown paint type %",
                                spec.Specification.Paint.Type));
                    }

                    var dashboardSpeakers = spec.Specification.FrontWindowSpeakers
                        .Select(s => new CarSpecification.SpeakerSpecification {IsSubwoofer = s.IsSubwoofer});
                    var doorSpeakers = spec.Specification.DoorSpeakers
                        .Select(s => new CarSpecification.SpeakerSpecification {IsSubwoofer = s.IsSubwoofer});
                    var wantedCar = new CarSpecification(
                        paint,
                        spec.Specification.Manufacturer,
                        spec.Specification.NumberOfDoors,
                        doorSpeakers,
                        dashboardSpeakers
                    );
                    wantedCars.Add(wantedCar);
                }
            }

            return wantedCars;
        }
    }
}