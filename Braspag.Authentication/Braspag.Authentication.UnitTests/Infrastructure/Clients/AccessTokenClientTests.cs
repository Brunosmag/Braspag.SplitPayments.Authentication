using AutoFixture.Idioms;
using Braspag.Authentication.Infrastructure.Clients;
using Braspag.Authentication.Infrastructure.Contracts;
using Braspag.Authentication.UnitTests.AutoFixture;
using Braspag.Authentication.UnitTests.NSubstitute;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Braspag.Authentication.UnitTests.Infrastructure.Clients
{
    public class AccessTokenClientTests
    {
        [Theory,AutoNSubstituteData]
        public void Sut_Should_Guard_Its_Clause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(AccessTokenClient).GetConstructors());
        }

        [Theory,AutoNSubstituteData]
        public void Sut_Should_Implement_IAccessTokenClient(AccessTokenClient sut)
        {
            Assert.IsAssignableFrom<IAccessTokenClient>(sut);
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_Should_RequestAsync_Correctly_When_Sandbox(
            string clientCredentialsInBase64,
            AccessToken sandboxAccessToken,
            AccessTokenClient sut)
        {
            var fakeHttpMessageWrapper = new FakeHttpMessageWrapper
            {
                HttpContent = new StringContent(JsonConvert.SerializeObject(sandboxAccessToken))
            };

            var httpClient = new HttpClient(fakeHttpMessageWrapper) { BaseAddress = new Uri("https://authsandbox.braspag.com.br") };
            sut.HttpClientFactory.CreateClient(Arg.Any<string>()).Returns(httpClient);

            var token = await sut.GetSandboxTokenAsync(clientCredentialsInBase64);

            sut.HttpClientFactory.Received().CreateClient();

            token.Should().BeEquivalentTo(sandboxAccessToken);
        }
    }
}
