namespace CarFactory_Domain.Engine
{
    public class EngineBlock
    {
        public EngineBlock(int steel)
        {
            CylinderCount = steel / RequiredSteelPerCylinder;
            Volume = steel;
        }

        public int CylinderCount { get; protected set; }
        public double Volume { get; protected set; }

        public static int RequiredSteelPerCylinder => 10;
    }
}
