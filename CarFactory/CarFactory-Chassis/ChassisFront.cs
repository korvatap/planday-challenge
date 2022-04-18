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
            switch (_typeId)
            {
                case 0:
                    return "Sportcar";
                case 1:
                    return "Offroader";
                case 2:
                    return "Family car";
                default:
                    throw new Exception("Unknown frontend type");
            }
        }

        public override string GetType()
        {
            return "ChassisFront";
        }
    }
}
