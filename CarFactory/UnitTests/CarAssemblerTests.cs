using System;
using System.Collections.Generic;
using CarFactory_Assembly;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class CarAssemblerTests
    {
        [Theory, AutoFakeData]
        public void CarAssembler_AssembleCar_ThrowsWhenNullChassis(Engine engine, Interior interior, List<Wheel> wheels) => 
            NullGuardClauseTest(null, engine, interior, wheels);
        
        [Theory, AutoFakeData]
        public void CarAssembler_AssembleCar_ThrowsWhenNullEngine(Chassis chassis, Interior interior, List<Wheel> wheels) => 
            NullGuardClauseTest(chassis, null, interior, wheels);
        
        [Theory, AutoFakeData]
        public void CarAssembler_AssembleCar_ThrowsWhenNullInterior(Chassis chassis, Engine engine, List<Wheel> wheels) => 
            NullGuardClauseTest(chassis, engine, null, wheels);
        
        [Theory, AutoFakeData]
        public void CarAssembler_AssembleCar_ThrowsWhenNullWheels(Chassis chassis, Engine engine, Interior interior) => 
            NullGuardClauseTest(chassis, engine, interior, null);

        [Theory, AutoFakeData]
        public void CarAssembler_AssembleCar_ThrowsWhenLessThanFourWheels(Chassis chassis, Engine engine, Interior interior)
        {
            // Arrange
            var wheels = Array.Empty<Wheel>();
            var sut = new CarAssembler();
            Action sutMethod = () => sut.AssembleCar(chassis, engine, interior, wheels);
            
            // Act & Assert
            sutMethod.Should().Throw<Exception>()
                .WithMessage("Common cars must have 4 wheels");
        }

        [Theory, AutoFakeData]
        public void CarAssembler_AssembleCar_Success(Chassis chassis, Engine engine, Interior interior, Wheel wheel)
        {
            // Arrange
            var wheels = new[] {wheel, wheel, wheel, wheel};
            var sut = new CarAssembler();
            
            // Act
            var actual = sut.AssembleCar(chassis, engine, interior, wheels);
            
            // Assert
            actual.Should().NotBeNull();
            actual.Chassis.Should().BeSameAs(chassis);
            actual.Engine.Should().BeSameAs(engine);
            actual.Interior.Should().BeSameAs(interior);
            actual.Wheels.Should().BeSameAs(wheels);
        }
        
        private static void NullGuardClauseTest(
            Chassis? chassis,
            Engine? engine,
            Interior? interior,
            IEnumerable<Wheel>? wheels)
        {
            // Arrange
            var sut = new CarAssembler();
            Action sutMethod = () => sut.AssembleCar(chassis, engine, interior, wheels);

            // Act & Assert
            sutMethod.Should().Throw<ArgumentNullException>();
        }
    }
}