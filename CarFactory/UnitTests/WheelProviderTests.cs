using System;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Storage;
using CarFactory_Wheels;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class WheelProviderTests
    {
        [Theory, AutoFakeData]
        public void WheelProvider_GetWheels_Success(IGetRubberQuery rubberQueryProvider, Part part)
        {
            // Arrange
            part.PartType = PartType.Rubber;
            A.CallTo(() => rubberQueryProvider.Get()).Returns(new []{part});
         
            // Act
            var wheelProvider = new WheelProvider(rubberQueryProvider);
            var actual = wheelProvider.GetWheels().ToList();

            // Assert
            actual.Count.Should().Be(4);
            actual.ForEach(w => w.Manufacturer.Should().Be(Manufacturer.Planborgini));
            A.CallTo(() => rubberQueryProvider.Get()).MustHaveHappenedOnceExactly();
        }

        [Theory, AutoFakeData]
        public void WheelProvider_GetWheels_ThrowsWhenReturningNotRubber(IGetRubberQuery rubberQueryProvider, Part part)
        {
            // Arrange
            part.PartType = PartType.Cotton;
            A.CallTo(() => rubberQueryProvider.Get()).Returns(new []{part});

            // Act
            var wheelProvider = new WheelProvider(rubberQueryProvider);
            Action action = () => wheelProvider.GetWheels();
            action.Should().Throw<Exception>().WithMessage("parts must be rubber");
            A.CallTo(() => rubberQueryProvider.Get()).MustHaveHappenedOnceExactly();
        }
        
        [Theory, AutoFakeData]
        public void WheelProvider_GetWheels_ThrowsWhenEmptyPartsList(IGetRubberQuery rubberQueryProvider)
        {
            // Arrange
            A.CallTo(() => rubberQueryProvider.Get()).Returns(Array.Empty<Part>());

            // Act
            var wheelProvider = new WheelProvider(rubberQueryProvider);
            Action action = () => wheelProvider.GetWheels();
            action.Should().Throw<Exception>().WithMessage("Sequence contains no elements");
            A.CallTo(() => rubberQueryProvider.Get()).MustHaveHappenedOnceExactly();
        }
    }
}