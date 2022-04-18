using System.Linq;
using CarFactory_Domain;
using CarFactory_Interior.Builders;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class SeatBuilderTests
    {
        [Fact]
        public void SeatBuilder_Build_Success()
        {
            // Arrange
            var seatBuilder = new SeatBuilder();

            // Act
            var seats = seatBuilder.Build().ToList();
            
            // Assert
            seats.Should().NotBeEmpty();
            seats.Count.Should().Be(4);
            seats.ForEach(s => s.PartType.Should().Be(PartType.Leather));
        }
    }
}