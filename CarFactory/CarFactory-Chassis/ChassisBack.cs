using System;

namespace CarFactory_Chassis
{
    public class ChassisBack : ChassisPart
    {
        public ChassisBack(int typeId) : base(typeId)
        {}

        public override string GetChassisType()
        {
            switch (_typeId)
            {
                case 0:
                    return "Sedan";
                case 1:
                    return "Pickup";
                case 2:
                    return "Hatchback";
                default:
                    throw new Exception("Unknown trunk type");
            }
        }

        public override string GetType()
        {
            return "ChassisBack";
        }

    }
}
