using System;
using System.Collections.Generic;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Interior.Interfaces;
using static CarFactory_Factory.CarSpecification;

namespace CarFactory_Interior.Builders
{
    public class SpeakerBuilder : ISpeakerBuilder
    {
        public List<Speaker> BuildFrontWindowSpeakers(IEnumerable<SpeakerSpecification> specification)
        {
            return specification.Select(spec =>
                new Speaker { IsSubwoofer = spec.IsSubwoofer }
            )
                .ToList();
        }
    }
}