using System.Collections.Generic;
using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Interior;
using CarFactory_Interior.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class InteriorProviderTests
    {
        [Theory, AutoFakeData]
        public void InteriorProvider_GetInterior_Success(
            IDashboardBuilder dashboardBuilder,
            ISeatBuilder seatBuilder,
            ISpeakerBuilder speakerBuilder,
            Dashboard dashboard,
            List<Seat> seats,
            List<Speaker> speakers,
            SingleColorPaintJob paintJob,
            Manufacturer manufacturer,
            int numberOfDoors,
            List<CarSpecification.SpeakerSpecification> speakerSpecifications)
        {
            // Arrange
            A.CallTo(() => dashboardBuilder.Build()).Returns(dashboard);
            A.CallTo(() => seatBuilder.Build()).Returns(seats);
            A.CallTo(() =>
                    speakerBuilder.BuildFrontWindowSpeakers(A<IEnumerable<CarSpecification.SpeakerSpecification>>._))
                .Returns(speakers);
            A.CallTo(() =>
                    speakerBuilder.BuildDoorSpeakers(A<IEnumerable<CarSpecification.SpeakerSpecification>>._))
                .Returns(speakers);
            var sut = new InteriorProvider(dashboardBuilder, seatBuilder, speakerBuilder);
            var carSpecification = new CarSpecification(
                paintJob,
                manufacturer,
                numberOfDoors,
                speakerSpecifications,
                speakerSpecifications
            );

            // Act
            var actual = sut.GetInterior(carSpecification);

            // Assert
            actual.Dashboard.Should().Be(dashboard);
            actual.Seats.Should().BeSameAs(seats);
            actual.DoorSpeakers.Should().BeSameAs(speakers);
            actual.FrontWindowSpeakers.Should().BeSameAs(speakers);
            A.CallTo(() => dashboardBuilder.Build()).MustHaveHappenedOnceExactly();
            A.CallTo(() => seatBuilder.Build()).MustHaveHappenedOnceExactly();
            A.CallTo(() =>
                    speakerBuilder.BuildFrontWindowSpeakers(A<IEnumerable<CarSpecification.SpeakerSpecification>>._))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => speakerBuilder.BuildDoorSpeakers(A<IEnumerable<CarSpecification.SpeakerSpecification>>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}