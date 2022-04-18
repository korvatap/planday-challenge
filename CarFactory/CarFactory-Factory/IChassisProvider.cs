using System.Threading.Tasks;
using CarFactory_Domain;

namespace CarFactory_Factory
{
    public interface IChassisProvider
    {
        Task<Chassis> GetChassis(Manufacturer manufacturer, int numberOfDoors);
    }
}