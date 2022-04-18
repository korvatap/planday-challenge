using System.Threading.Tasks;
using CarFactory_Domain;
using CarFactory_Domain.Engine;

namespace CarFactory_Factory
{
    public interface IEngineProvider
    {
        Task<Engine> GetEngine(Manufacturer manufacturer);
    }
}