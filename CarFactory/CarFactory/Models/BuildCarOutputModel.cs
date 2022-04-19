using System.Collections.Generic;
using CarFactory_Domain;

namespace CarFactory.Models
{
    public sealed record BuildCarOutputModel(long RunTime, IEnumerable<Car> Cars);
}
