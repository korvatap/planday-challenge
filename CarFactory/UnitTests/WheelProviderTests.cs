using System;
using System.Collections.Generic;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Storage;
using CarFactory_Wheels;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class WheelProviderTests
    {
        private Mock<IGetRubberQuery> _rubberQueryProvider;

        [TestInitialize()]
        public void TestInitialize()
        {
            _rubberQueryProvider = new Mock<IGetRubberQuery>();
        }

        [TestMethod]
        public void WheelProvider_GetWheels_Success()
        {
            // Arrange
            _rubberQueryProvider.Setup(p => p.Get())
                .Returns(new List<Part>
                {
                    new() {Manufacturer = Manufacturer.Planborgini, PartType = PartType.Rubber}
                });

            // Act
            var wheelProvider = new WheelProvider(_rubberQueryProvider.Object);
            var actual = wheelProvider.GetWheels().ToList();

            // Assert
            actual.Count.Should().Be(4);
            actual.ForEach(w => w.Manufacturer.Should().Be(Manufacturer.Planborgini));
        }

        [TestMethod]
        public void WheelProvider_GetWheels_ThrowsWhenReturningNotRubber()
        {
            // Arrange
            _rubberQueryProvider.Setup(p => p.Get())
                .Returns(new List<Part>
                {
                    new() {Manufacturer = Manufacturer.Planborgini, PartType = PartType.Cotton}
                });

            // Act
            var wheelProvider = new WheelProvider(_rubberQueryProvider.Object);
            Action action = () => wheelProvider.GetWheels();
            action.Should().Throw<Exception>().WithMessage("parts must be rubber");
        }
        
        [TestMethod]
        public void WheelProvider_GetWheels_ThrowsWhenEmptyPartsList()
        {
            // Arrange
            _rubberQueryProvider.Setup(p => p.Get()).Returns(new List<Part>());

            // Act
            var wheelProvider = new WheelProvider(_rubberQueryProvider.Object);
            Action action = () => wheelProvider.GetWheels();
            action.Should().Throw<Exception>().WithMessage("Sequence contains no elements");
        } 
    }
}