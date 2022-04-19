using System;

namespace CarFactory_Chassis
{
    public class ChassisBack : ChassisPart
    {
        public ChassisBack(int typeId) : base(typeId)
        {}

        public override string GetChassisType()
        {
            return TypeId switch
            {
                0 => "Sedan",
                1 => "Pickup",
                2 => "Hatchback",
                _ => throw new Exception("Unknown trunk type")
            };
        }

        public override string GetType()
        {
            return "ChassisBack";
        }

    }
}
