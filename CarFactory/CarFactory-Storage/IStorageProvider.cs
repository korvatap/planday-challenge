using System.Data.SQLite;

namespace CarFactory_Storage
{
    public interface IStorageProvider
    {
        public SQLiteConnection GetConnection();
    }
}
