using AutoFixture;
using AutoFixture.AutoFakeItEasy;

namespace UnitTests
{
    public class AutoFakeCustomization : CompositeCustomization
    {
        public AutoFakeCustomization()
            : base(new AutoFakeItEasyCustomization {GenerateDelegates = true})
        {
        }
    }
}