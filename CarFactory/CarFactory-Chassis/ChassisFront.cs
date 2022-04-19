using System;

namespace CarFactory_Chassis
{
    public class ChassisFront : ChassisPart
    {
        public ChassisFront(int typeId) : base(typeId)
        {

        }
        public override string GetChassisType()
        {
            return TypeId switch
            {
                0 => "Sportcar",
                1 => "Offroader",
                2 => "Family car",
                _ => throw new Exception("Unknown frontend type")
            };
        }

        public override string GetType()
        {
            return "ChassisFront";
        }
    }
}
