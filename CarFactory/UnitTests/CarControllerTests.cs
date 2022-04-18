using System;
using System.Collections.Generic;
using System.Drawing;
using CarFactory.Controllers;
using CarFactory.Mappers;
using CarFactory.Models;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Factory;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace UnitTests
{
    public class CarControllerTests
    {
        [Theory, AutoFakeData]
        public void CarController_Post_ReturnsExpected(
            ICarFactory carFactory,
            ICarSpecificationMapper carSpecificationMapper,
            BuildCarInputModel inputModel,
            Chassis chassis,
            Engine engine,
            Interior interior,
            List<Wheel> wheels)
        {
            // Arrange
            var carSpecifications = new List<CarSpecification>
            {
                new(
                    new SingleColorPaintJob(Color.Aqua),
                    Manufacturer.Planborgini,
                    5,
                    Array.Empty<CarSpecification.SpeakerSpecification>(),
                    Array.Empty<CarSpecification.SpeakerSpecification>())
            };
            var expectedCars = new List<Car> {new Car(chassis, engine, interior, wheels)};
            A.CallTo(() => carSpecificationMapper.Map(inputModel)).Returns(carSpecifications);
            A.CallTo(() => carFactory.BuildCars(carSpecifications)).Returns(expectedCars);
            var sut = new CarController(carFactory, carSpecificationMapper);

            // Act
            var response = sut.Post(inputModel);

            // Assert
            response.Should().BeOfType<BuildCarOutputModel>();
            var result = response as BuildCarOutputModel;
            result!.Cars.Should().BeSameAs(expectedCars);

            A.CallTo(() => carSpecificationMapper.Map(A<BuildCarInputModel>._))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => carFactory.BuildCars(A<IEnumerable<CarSpecification>>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}