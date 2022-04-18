using System;
using System.Collections.Generic;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Paint;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class PainterTests
    {
        [Theory, AutoFakeData]
        public void Painter_PaintJobTest(
            SingleColorPaintJob singleColor,
            Engine engine,
            Interior interior,
            Chassis chassis,
            List<Wheel> wheels)
        {
            // Arrange
            var painter = new Painter();
            var car = new Car(chassis, engine, interior, wheels);
            
            // Act
            painter.PaintCar(car, singleColor);
            
            // Assert
            var job = (SingleColorPaintJob) car.PaintJob;
            job.Color.Should().Be(singleColor.Color);
            job.AreInstructionsUnlocked().Should().BeTrue();
        }

        [Theory, AutoFakeData]
        public void Painter_PaintJob_ThrowsExceptionWhenNoChassis(
            SingleColorPaintJob singleColor,
            Engine engine,
            Interior interior,
            List<Wheel> wheels)
        {
            // Arrange
            var painter = new Painter();
            var car = new Car(null, engine, interior, wheels);
            
            // Act & Assert
            Action action = () => painter.PaintCar(car, singleColor);
            action.Should().Throw<Exception>("Cannot paint a car without chassis");
        }
    }
}