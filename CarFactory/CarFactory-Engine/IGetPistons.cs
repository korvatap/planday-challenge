using System.Threading.Tasks;

namespace CarFactory_Engine
{
    public interface IGetPistons
    {
        Task<int> Get(int amount);   
    }
}