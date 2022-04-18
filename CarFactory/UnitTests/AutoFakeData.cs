using System;
using AutoFixture;
using AutoFixture.Xunit2;

namespace UnitTests
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AutoFakeDataAttribute : AutoDataAttribute
    {
        public AutoFakeDataAttribute()
            : base(() => new Fixture().Customize(new AutoFakeCustomization()))
        {
        }
    }
}