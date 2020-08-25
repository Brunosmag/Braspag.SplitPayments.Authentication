using AutoFixture;
using AutoFixture.Xunit2;


namespace Braspag.Authentication.UnitTests.AutoFixture
{
    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute() : base(FixtureFactory)
        {
        }

        public static IFixture FixtureFactory()
        {
            var fixture = new Fixture();

            return fixture;
        }
    }
}
