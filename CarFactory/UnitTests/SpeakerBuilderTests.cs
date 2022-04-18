using System;
using System.Collections.Generic;
using System.Linq;
using CarFactory_Factory;
using CarFactory_Interior.Builders;
using FluentAssertions;
using Xunit;

namespace UnitTests
{
    public class SpeakerBuilderTests
    {
        [Theory, AutoFakeData]
        public void SpeakerBuilder_BuildFrontWindowSpeakers_ThrowsWhenMoreThanTwoSpeakers(
            CarSpecification.SpeakerSpecification speakerSpecification)
        {
            // Arrange
            var speakerBuilder = new SpeakerBuilder();
            var speakerSpecifications = new List<CarSpecification.SpeakerSpecification>()
            {
                speakerSpecification,
                speakerSpecification,
                speakerSpecification
            };

            // Act
            Action sutMethod = () => speakerBuilder.BuildFrontWindowSpeakers(speakerSpecifications);
            sutMethod.Should().Throw<ArgumentException>()
                .WithMessage("More than 2 speakers aren't supported");
        }

        [Theory, AutoFakeData]
        public void SpeakerBuilder_BuildFrontWindowSpeakers_Success(
            CarSpecification.SpeakerSpecification speakerSpecification)
        {
            // Arrange
            var speakerBuilder = new SpeakerBuilder();
            var speakerSpecifications = new List<CarSpecification.SpeakerSpecification>()
            {
                speakerSpecification
            };

            // Act
            var speakers = speakerBuilder.BuildFrontWindowSpeakers(speakerSpecifications);
            
            // Assert
            speakers.Should().NotBeEmpty();
            speakers.First().IsSubwoofer.Should().Be(speakerSpecification.IsSubwoofer);
        }
        
        [Theory, AutoFakeData]
        public void SpeakerBuilder_BuildDoorSpeakers_Success(
            CarSpecification.SpeakerSpecification speakerSpecification)
        {
            // Arrange
            var speakerBuilder = new SpeakerBuilder();
            var speakerSpecifications = new List<CarSpecification.SpeakerSpecification>()
            {
                speakerSpecification
            };

            // Act
            var speakers = speakerBuilder.BuildDoorSpeakers(speakerSpecifications);
            
            // Assert
            speakers.Should().NotBeEmpty();
            speakers.First().IsSubwoofer.Should().Be(speakerSpecification.IsSubwoofer);
        }
    }
}