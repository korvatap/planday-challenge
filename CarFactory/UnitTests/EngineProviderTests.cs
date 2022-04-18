using System.Collections.Generic;
using CarFactory_Domain;
using CarFactory_Domain.Engine.EngineSpecifications;
using CarFactory_Engine;
using CarFactory_Storage;
using CarFactory_SubContractor;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace UnitTests
{
    public class EngineProviderTests
    {
        [Theory, AutoFakeData]
        public void EngineProvider_GetEngine_GasolineV6_Success(
            IGetPistons getPistons,
            ISteelSubcontractor steelSubContractor,
            IGetEngineSpecificationQuery getEngineSpecification,
            IMemoryCache cache,
            Manufacturer manufacturer,
            List<SteelDelivery> steelDeliveries,
            int expectedPistons) =>
            BasicGetEngineTest(
                getPistons,
                steelSubContractor,
                getEngineSpecification,
                cache,
                manufacturer,
                steelDeliveries,
                expectedPistons,
                new GasolineV8(),
                true
            );

        [Theory, AutoFakeData]
        public void EngineProvider_GetEngine_GasolineV8_Success(
            IGetPistons getPistons,
            ISteelSubcontractor steelSubContractor,
            IGetEngineSpecificationQuery getEngineSpecification,
            IMemoryCache cache,
            Manufacturer manufacturer,
            List<SteelDelivery> steelDeliveries,
            int expectedPistons) =>
            BasicGetEngineTest(
                getPistons,
                steelSubContractor,
                getEngineSpecification,
                cache,
                manufacturer,
                steelDeliveries,
                expectedPistons,
                new GasolineV8(),
                true
            );

        [Theory, AutoFakeData]
        public void EngineProvider_GetEngine_GasolineV12_Success(
            IGetPistons getPistons,
            ISteelSubcontractor steelSubContractor,
            IGetEngineSpecificationQuery getEngineSpecification,
            IMemoryCache cache,
            Manufacturer manufacturer,
            List<SteelDelivery> steelDeliveries,
            int expectedPistons) =>
            BasicGetEngineTest(
                getPistons,
                steelSubContractor,
                getEngineSpecification,
                cache,
                manufacturer,
                steelDeliveries,
                expectedPistons,
                new GasolineV12(),
                true
            );

        [Theory, AutoFakeData]
        public void EngineProvider_GetEngine_Diesel_Success(
            IGetPistons getPistons,
            ISteelSubcontractor steelSubContractor,
            IGetEngineSpecificationQuery getEngineSpecification,
            IMemoryCache cache,
            Manufacturer manufacturer,
            List<SteelDelivery> steelDeliveries,
            int expectedPistons) =>
            BasicGetEngineTest(
                getPistons,
                steelSubContractor,
                getEngineSpecification,
                cache,
                manufacturer,
                steelDeliveries,
                expectedPistons,
                new DieselStraight4(),
                false
            );

        private static void BasicGetEngineTest(
            IGetPistons getPistons,
            ISteelSubcontractor steelSubContractor,
            IGetEngineSpecificationQuery getEngineSpecification,
            IMemoryCache cache,
            Manufacturer manufacturer,
            List<SteelDelivery> steelDeliveries,
            int expectedPistons,
            EngineSpecification engineSpecification,
            bool hasSparkPlugs)
        {
            // Arrange
            var sut = new EngineProvider(getPistons, steelSubContractor, getEngineSpecification, cache);
            A.CallTo(() => getEngineSpecification.GetForManufacturer(manufacturer))
                .Returns(engineSpecification);
            A.CallTo(() => steelSubContractor.OrderSteel(A<int>._)).Returns(steelDeliveries);
            A.CallTo(() => getPistons.Get(A<int>._)).Returns(expectedPistons);

            // Act
            var engine = sut.GetEngine(manufacturer);

            // Assert
            engine.IsFinished.Should().BeTrue();
            engine.EngineSpecification.Should().Be(engineSpecification.Name);
            engine.EngineBlock.CylinderCount.Should().Be(engineSpecification.CylinderCount);
            engine.PistonsCount.Should().Be(expectedPistons);
            engine.HasSparkPlugs.Should().Be(hasSparkPlugs);
            A.CallTo(() => getEngineSpecification.GetForManufacturer(manufacturer))
                .MustHaveHappenedOnceExactly();
            A.CallTo(() => steelSubContractor.OrderSteel(A<int>._)).MustHaveHappened();
        }
    }
}