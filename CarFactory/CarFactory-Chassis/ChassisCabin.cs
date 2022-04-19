using System;

namespace CarFactory_Chassis
{
    public class ChassisCabin : ChassisPart
    {
        public ChassisCabin(int typeId) : base(typeId)
        {

        }

        public override string GetChassisType()
        {
            return TypeId switch
            {
                0 => "Two Door",
                1 => "Four Door",
                _ => throw new Exception("Unknown cabin type")
            };
        }

        public override string GetType() => "ChassisCabin";
    }

}
