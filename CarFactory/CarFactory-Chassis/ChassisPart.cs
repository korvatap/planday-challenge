namespace CarFactory_Chassis
{
    public abstract class ChassisPart
    {
        protected readonly int TypeId;

        protected ChassisPart(int typeId)
        {
            TypeId = typeId;
        }
        
        public new abstract string GetType();

        public abstract string GetChassisType();
    }

    
}
