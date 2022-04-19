using CarFactory_Domain;

namespace CarFactory.Models
{
    public sealed record CarSpecificationInputModel(
        int NumberOfDoors,
        CarPaintSpecificationInputModel Paint,
        Manufacturer Manufacturer,
        SpeakerSpecificationInputModel[] FrontWindowSpeakers,
        SpeakerSpecificationInputModel[] DoorSpeakers
    );
}