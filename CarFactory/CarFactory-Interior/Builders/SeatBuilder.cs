using System.Collections.Generic;
using CarFactory_Domain;
using CarFactory_Interior.Interfaces;

namespace CarFactory_Interior.Builders
{
    public class SeatBuilder : ISeatBuilder
    {
        public IEnumerable<Seat> Build() =>
            new List<Seat>
            {
                new() {PartType = PartType.Leather},
                new() {PartType = PartType.Leather},
                new() {PartType = PartType.Leather},
                new() {PartType = PartType.Leather}
            };
    }
}