using System.Threading.Tasks;
using CarFactory.Utilities;

namespace CarFactory_Engine
{
    public class GetPistons : IGetPistons
    {
        public Task<int> Get(int amount)
        {
            return Task.Run(() =>
            {
                SlowWorker.FakeWorkingForMillis(amount * 50);
                return amount;
            });
        }
    }
}
