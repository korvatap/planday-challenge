using System.Threading.Tasks;
using CarFactory_Engine;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class GetPistonsTests
    {
        [Fact]
        public async Task GetPistons_Success()
        {
            // Arrange
            var sut = new GetPistons();
            
            // Act
            var pistons = await sut.Get(1);
            
            // Assert
            pistons.Should().Be(1);
        }
    }
}