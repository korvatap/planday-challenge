using CarFactory_Domain;
using System.Collections.Generic;

namespace CarFactory_Factory
{
    public class CarSpecification
    {
        public readonly int NumberOfDoors;
        public readonly PaintJob PaintJob;
        public readonly Manufacturer Manufacturer;
        public readonly IEnumerable<SpeakerSpecification> DoorSpeakers;
        public readonly IEnumerable<SpeakerSpecification> FrontWindowSpeakers;

        public CarSpecification(PaintJob paint, Manufacturer manufacturer, int numberOfDoors, IEnumerable<SpeakerSpecification> doorSpeakers, IEnumerable<SpeakerSpecification> dashboardSpeakers)
        {
            PaintJob = paint;
            Manufacturer = manufacturer;
            NumberOfDoors = numberOfDoors;
            DoorSpeakers = doorSpeakers;
            FrontWindowSpeakers = dashboardSpeakers;
        }

        public class SpeakerSpecification
        {
            public bool IsSubwoofer;
        }
    }
}
