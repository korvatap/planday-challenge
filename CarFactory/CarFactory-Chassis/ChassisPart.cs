namespace CarFactory_Chassis
{
    public abstract class ChassisPart
    {
        public readonly int _typeId;
        public ChassisPart(int typeId)
        {
            _typeId = typeId;
        }
        public new abstract string GetType();

        public abstract string GetChassisType();
    }

    
}
