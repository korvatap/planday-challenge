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
            var specifications = specification.ToArray();
            if (specifications.Length > 2) throw new ArgumentException("More than 2 speakers aren't supported");
            return BuildDoorSpeakers(specifications);
        }

        public List<Speaker> BuildDoorSpeakers(IEnumerable<SpeakerSpecification> specification) =>
            specification
                .Select(spec => new Speaker { IsSubwoofer = spec.IsSubwoofer })
                .ToList();
    }
}