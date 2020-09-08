using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

            fixture.Customize(new AutoNSubstituteCustomization { ConfigureMembers = true })
                .Customize<BindingInfo>(c => c.OmitAutoProperties());

            return fixture;
        }
    }
}
