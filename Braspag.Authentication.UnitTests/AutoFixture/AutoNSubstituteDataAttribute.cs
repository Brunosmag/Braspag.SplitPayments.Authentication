using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using Braspag.Authentication.Infrastructure.Contracts;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

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

            fixture.Customize<AccessToken>(
                c =>
                    c.With(x => x.ExpiresIn, Math.Abs(fixture.Create<double>())));


            fixture.Customize(new AutoNSubstituteCustomization { ConfigureMembers = true })
                .Customize<BindingInfo>(c => c.OmitAutoProperties());

            return fixture;
        }
    }
}
