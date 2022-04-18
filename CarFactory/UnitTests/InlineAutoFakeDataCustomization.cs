using System;
using AutoFixture.Xunit2;
using Xunit;

namespace UnitTests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InlineAutoFakeDataAttribute : CompositeDataAttribute
    {
        public InlineAutoFakeDataAttribute(params object?[] values) : base(new InlineDataAttribute(values),
            new AutoFakeDataAttribute())
        {
        }
    }
}