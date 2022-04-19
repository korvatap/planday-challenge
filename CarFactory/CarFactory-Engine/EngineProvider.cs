using CarFactory.Utilities;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarFactory_Engine
{
    public class EngineProvider : IEngineProvider
    {
        private readonly IGetPistons _getPistons;
        private readonly ISteelSubcontractor _steelSubContractor;
        private int _steelInventory;
        private readonly IGetEngineSpecificationQuery _getEngineSpecification;
        private readonly IMemoryCache _cache;

        private static readonly MemoryCacheEntryOptions MemoryCacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));

        public EngineProvider(
            IGetPistons getPistons,
            ISteelSubcontractor steelSubContractor,
            IGetEngineSpecificationQuery getEngineSpecification,
            IMemoryCache cache)
        {
            _getPistons = getPistons;
            _steelSubContractor = steelSubContractor;
            _getEngineSpecification = getEngineSpecification;
            _cache = cache;
        }

        public async Task<Engine> GetEngine(Manufacturer manufacturer)
        {
            if (_cache.TryGetValue(manufacturer, out Engine cachedEngine))
            {
                return cachedEngine;
            }
            
            var specification = _getEngineSpecification.GetForManufacturer(manufacturer);

            var engineBlockTask = MakeEngineBlock(specification.CylinderCount);
            var pistonsTask = _getPistons.Get(specification.CylinderCount);

            await Task.WhenAll(engineBlockTask, pistonsTask);
            
            var engine = new Engine(engineBlockTask.Result, specification.Name);

            var installPistonsTask = InstallPistons(engine, pistonsTask.Result);
            var installFuelInjectorsTask = InstallFuelInjectors(engine, specification.PropulsionType);

            await Task.WhenAll(installPistonsTask, installFuelInjectorsTask);

            InstallSparkPlugs(engine);

            if (!engine.IsFinished)
                throw new InvalidOperationException("Cannot return an unfinished engine");

            _cache.Set(manufacturer, engine, MemoryCacheEntryOptions);

            return engine;
        }

        private Task<EngineBlock> MakeEngineBlock(int cylinders)
        {
            return Task.Run(() =>
            {
                var requiredSteel = cylinders * EngineBlock.RequiredSteelPerCylinder;

                var steel = GetSteel(requiredSteel);

                var engineBlock = new EngineBlock(steel);

                if (cylinders != engineBlock.CylinderCount)
                    throw new InvalidOperationException("Engine block does not have the required amount of cylinders");

                return engineBlock;
            });
        }

        private int GetSteel(int amount)
        {
            if (amount > _steelInventory)
            {
                var missingSteel = amount - _steelInventory;
                _steelInventory += _steelSubContractor.OrderSteel(missingSteel).Sum(sd => sd.Amount);
            }

            _steelInventory -= amount;

            return amount;
        }

        private Task InstallFuelInjectors(Engine engine, Propulsion propulsionType)
        {
            //Do work
            return Task.Run(() =>
            {
                SlowWorker.FakeWorkingForMillis(24 * engine.EngineBlock.CylinderCount);
                engine.PropulsionType = propulsionType;
            });
        }

        private Task InstallPistons(Engine engine, int pistons)
        {
            //Do work
            return Task.Run(() =>
            {
                SlowWorker.FakeWorkingForMillis(25 * pistons);
                engine.PistonsCount = pistons;
            });
        }

        private void InstallSparkPlugs(Engine engine)
        {
            if (!engine.PropulsionType.HasValue)
                throw new InvalidOperationException($"Propulsion type must be known before installing spark plugs");

            SlowWorker.FakeWorkingForMillis(engine.EngineBlock.CylinderCount * 15);
            engine.HasSparkPlugs = true;
        }
    }
}