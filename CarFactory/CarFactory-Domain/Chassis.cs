namespace CarFactory_Domain
{
    public class Chassis
    {
        public Chassis(string description, bool valid)
        {
            Description = description;
            ValidConstruction = valid;
        }
        public string Description { get; }
        public bool ValidConstruction { get; }
    }
}
