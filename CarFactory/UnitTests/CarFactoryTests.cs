using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Factory;
using CarFactory_Storage;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class CarFactoryTests
    {
        [Theory, AutoFakeData]
        public async Task CarFactory_Test(
            IChassisProvider chassisProvider,
            IEngineProvider engineProvider,
            IPainter painter,
            IInteriorProvider interiorProvider,
            IWheelProvider wheelProvider,
            ICarAssembler carAssembler,
            IStorageProvider storageProvider,
            SingleColorPaintJob paintJob,
            Manufacturer manufacturer,
            int numberOfDoors,
            List<CarSpecification.SpeakerSpecification> speakerSpecifications,
            Chassis chassis,
            Engine engine,
            Interior interior,
            List<Wheel> wheels)
        {
            // Arrange
            var sut = new CarFactory_Factory.CarFactory(
                chassisProvider,
                engineProvider,
                painter,
                interiorProvider,
                wheelProvider,
                carAssembler,
                storageProvider
            );

            var carSpecification = new CarSpecification(
                paintJob,
                manufacturer,
                numberOfDoors,
                speakerSpecifications,
                speakerSpecifications
            );

            var expected = new Car(chassis, engine, interior, wheels);
            A.CallTo(() => painter.PaintCar(A<Car>._, A<PaintJob>._)).Returns(expected);

            // Act
            var cars = (await sut.BuildCars(new[] {carSpecification})).ToList();

            // Assert
            cars.Count.Should().Be(1);
            cars.First().Should().BeSameAs(expected);
            A.CallTo(() => chassisProvider.GetChassis(A<Manufacturer>._, A<int>._))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => engineProvider.GetEngine(A<Manufacturer>._))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => interiorProvider.GetInterior(A<CarSpecification>._))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => wheelProvider.GetWheels())
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => carAssembler.AssembleCar(A<Chassis>._, A<Engine>._, A<Interior>._, A<IEnumerable<Wheel>>._))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => painter.PaintCar(A<Car>._, A<PaintJob>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}