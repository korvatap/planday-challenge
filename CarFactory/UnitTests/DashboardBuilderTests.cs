using CarFactory_Domain;
using CarFactory_Interior.Builders;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class DashboardBuilderTests
    {
        [Fact]
        public void DashboardBuilder_Build_Success()
        {
            // Arrange
            var seatBuilder = new DashboardBuilder();

            // Act
            var dashboard = seatBuilder.Build();
            
            // Assert
            dashboard.PartType.Should().Be(PartType.Wood);
            dashboard.HasTouchScreen.Should().BeTrue();
            dashboard.HasGPS.Should().BeTrue();
        }
    }
}