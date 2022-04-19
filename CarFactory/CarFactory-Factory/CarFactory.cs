using CarFactory_Domain;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using CarFactory_Storage;

namespace CarFactory_Factory
{
    public class CarFactory : ICarFactory
    {
        private readonly IChassisProvider _chassisProvider;
        private readonly IEngineProvider _engineProvider;
        private readonly IPainter _painter;
        private readonly IInteriorProvider _interiorProvider;
        private readonly IWheelProvider _wheelProvider;
        private readonly ICarAssembler _carAssembler;
        private IStorageProvider _storageProvider;

        public CarFactory(
            IChassisProvider chassisProvider,
            IEngineProvider engineProvider,
            IPainter painter,
            IInteriorProvider interiorProvider,
            IWheelProvider wheelProvider,
            ICarAssembler carAssembler,
            IStorageProvider storageProvider)
        {
            _chassisProvider = chassisProvider;
            _engineProvider = engineProvider;
            _painter = painter;
            _interiorProvider = interiorProvider;
            _wheelProvider = wheelProvider;
            _carAssembler = carAssembler;
            _storageProvider = storageProvider;
        }

        public async Task<IEnumerable<Car>> BuildCars(IEnumerable<CarSpecification> specs)
        {
            var cars = new List<Car>();

            if (SynchronizationContext.Current is null)
            {
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            }

            await specs.AsyncParallelForEach(async spec =>
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var chassisTask = _chassisProvider.GetChassis(spec.Manufacturer, spec.NumberOfDoors);
                    var engineTask = _engineProvider.GetEngine(spec.Manufacturer);
                    await Task.WhenAll(chassisTask, engineTask);
                    var interior = _interiorProvider.GetInterior(spec);
                    var wheels = _wheelProvider.GetWheels();
                    var car = _carAssembler.AssembleCar(chassisTask.Result, engineTask.Result, interior, wheels);
                    var paintedCar = _painter.PaintCar(car, spec.PaintJob);
                    cars.Add(paintedCar);
                    stopwatch.Stop();
                    Console.WriteLine($"Elapsed: {stopwatch.ElapsedMilliseconds}");
                }, -1, TaskScheduler.FromCurrentSynchronizationContext()
            );

            return cars;
        }
    }

    public static class AsyncExtensions
    {
        public static Task AsyncParallelForEach<T>(
            this IEnumerable<T> source,
            Func<T, Task> body,
            int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded,
            TaskScheduler scheduler = null)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };
            
            if (scheduler != null)
                options.TaskScheduler = scheduler;

            var block = new ActionBlock<T>(body, options);

            foreach (var item in source)
                block.Post(item);

            block.Complete();
            return block.Completion;
        }
    }
}