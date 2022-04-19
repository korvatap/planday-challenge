using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CarFactory.Mappers;
using CarFactory.Models;
using CarFactory_Domain;
using CarFactory_Factory;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class CarSpecificationMapperTests
    {
        [Fact]
        public void CarSpecificationMapper_Map_Success()
        {
            // Arrange
            var cars = new List<BuildCarInputModelItem>();
            cars.AddRange(GetInputModel(75, Manufacturer.PlanfaRomeo, 5, "stripe", "blue", "orange", null));
            cars.AddRange(GetInputModel(15, Manufacturer.Planborgini, 3, "dot", "pink", null, "red"));
            cars.AddRange(GetInputModel(20, Manufacturer.Volksday, 5, "stripe", "red", "black", null));
            cars.AddRange(GetInputModel(40, Manufacturer.PlandayMotorWorks, 3, "dot", "black", null, "yellow"));
            cars.AddRange(GetInputModel(20, Manufacturer.Plandrover, 5, "stripe", "green", "gold", null));

            var inputModels = new BuildCarInputModel(cars);
            var sut = new CarSpecificationMapper();

            // Act
            var actual = sut.Map(inputModels).ToList();

            // Assert
            AssertSuccessfulMap(actual, 75, Manufacturer.PlanfaRomeo, 5, Color.Blue, Color.Orange, null);
            AssertSuccessfulMap(actual, 15, Manufacturer.Planborgini, 3, Color.Pink, null, Color.Red);
            AssertSuccessfulMap(actual, 20, Manufacturer.Volksday, 5, Color.Red, Color.Black, null);
            AssertSuccessfulMap(actual, 40, Manufacturer.PlandayMotorWorks, 3, Color.Black, null, Color.Yellow);
            AssertSuccessfulMap(actual, 20, Manufacturer.Plandrover, 5, Color.Green, Color.Gold, null);
        }

        private static void AssertSuccessfulMap(
            IEnumerable<CarSpecification> carSpecifications,
            int expectedAmount,
            Manufacturer expectedManufacturer,
            int numberOfDoors,
            Color expectedBaseColor,
            Color? expectedStripeColor,
            Color? expectedDotColor)
        {
            var filtered = carSpecifications.Where(cs => cs.Manufacturer == expectedManufacturer).ToList();
            filtered.Count.Should().Be(expectedAmount);
            foreach (var cs in filtered)
            {
                cs.Manufacturer.Should().Be(expectedManufacturer);
                if (cs.PaintJob is StripedPaintJob)
                    AssertStripedPaintJob(cs.PaintJob, expectedBaseColor, expectedStripeColor!.Value);
                if (cs.PaintJob is DottedPaintJob)
                    AssertDottedPaintJob(cs.PaintJob, expectedBaseColor, expectedDotColor!.Value);
                if (cs.PaintJob is SingleColorPaintJob) AssertSinglePaintJob(cs.PaintJob, expectedBaseColor);
                cs.NumberOfDoors.Should().Be(numberOfDoors);
                var doorSpeakers = cs.DoorSpeakers.ToList();
                var windowSpeakers = cs.FrontWindowSpeakers.ToList();
                doorSpeakers.Count.Should().Be(1);
                doorSpeakers.First().IsSubwoofer.Should().BeTrue();
                windowSpeakers.Count.Should().Be(1);
                windowSpeakers.First().IsSubwoofer.Should().BeFalse();
            }
        }

        private static void AssertSinglePaintJob(PaintJob paintJob, Color expectedBaseColor)
        {
            var singleColorPaintJob = (SingleColorPaintJob) paintJob;
            singleColorPaintJob.Color.Should().Be(expectedBaseColor);
        }

        private static void AssertStripedPaintJob(PaintJob paintJob, Color expectedBaseColor, Color expectedStripeColor)
        {
            var stripedJob = (StripedPaintJob) paintJob;
            stripedJob.BaseColor.Should().Be(expectedBaseColor);
            stripedJob.StripeColor.Should().Be(expectedStripeColor);
        }

        private static void AssertDottedPaintJob(PaintJob paintJob, Color expectedBaseColor, Color expectedDotColor)
        {
            var dottedJob = (DottedPaintJob) paintJob;
            dottedJob.BaseColor.Should().Be(expectedBaseColor);
            dottedJob.DotColor.Should().Be(expectedDotColor);
        }

        private static IEnumerable<BuildCarInputModelItem> GetInputModel(
            int amount,
            Manufacturer manufacturer,
            int numberOfDoors,
            string type,
            string baseColor,
            string? stripeColor,
            string? dotColor)
        {
            return new List<BuildCarInputModelItem>
            {
                new(
                    amount,
                    new CarSpecificationInputModel(
                        numberOfDoors,
                        new CarPaintSpecificationInputModel(baseColor, type, stripeColor, dotColor),
                        manufacturer,
                        new[] {new SpeakerSpecificationInputModel(true)},
                        new[] {new SpeakerSpecificationInputModel(false)}
                    )
                )
            };
        }
    }
}