using System;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Paint;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace UnitTests
{
    [TestClass]
    public class PainterTests
    {
        [TestMethod]
        public void Painter_PaintJobTest()
        {
            // Arrange
            var singleColor = new SingleColorPaintJob(Color.Aqua);
            var painter = new Painter();
            var car = new Car(new Chassis("", true), new Engine(new EngineBlock(10), "Test"), new Interior(),
                new Wheel[4]);
            
            // Act
            painter.PaintCar(car, singleColor);
            
            // Assert
            var job = (SingleColorPaintJob) car.PaintJob;
            job.Color.Should().Be(Color.Aqua);
            job.AreInstructionsUnlocked().Should().BeTrue();
        }

        [TestMethod]
        public void Painter_PaintJob_ThrowsExceptionWhenNoChassis()
        {
            // Arrange
            var singleColor = new SingleColorPaintJob(Color.Aqua);
            var painter = new Painter();
            var car = new Car(null, new Engine(new EngineBlock(10), "Test"), new Interior(), new Wheel[4]);
            
            // Act & Assert
            Action action = () => painter.PaintCar(car, singleColor);
            action.Should().Throw<Exception>("Cannot paint a car without chassis");
        }
    }
}