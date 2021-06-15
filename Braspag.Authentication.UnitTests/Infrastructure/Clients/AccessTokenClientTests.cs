using AutoFixture;
using AutoFixture.Idioms;
using Braspag.Authentication.Infrastructure.Clients;
using Braspag.Authentication.Infrastructure.Contracts;
using Braspag.Authentication.Infrastructure.Handlers;
using Braspag.Authentication.UnitTests.AutoFixture;
using Braspag.Authentication.UnitTests.NSubstitute;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Braspag.Authentication.UnitTests.Infrastructure.Clients
{
    public class AccessTokenClientTests
    {
        private readonly Fixture _fixture;

        private readonly HttpClient _httpClientSubstitute;

        private readonly AccessTokenClient _sut;

        private readonly IHttpResponseMessageHandler _httpResponseMessageHandler;


        public AccessTokenClientTests()
        {
            _fixture = new Fixture();

            _httpClientSubstitute = Substitute.For<HttpClient>();

            _httpResponseMessageHandler = Substitute.For<IHttpResponseMessageHandler>();

            _sut = Substitute.ForPartsOf<AccessTokenClient>(_httpClientSubstitute, _httpResponseMessageHandler);

        }

        [Theory, AutoNSubstituteData]
        public void Sut_Should_Guard_Its_Clause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(AccessTokenClient).GetConstructors());
        }

        [Theory, AutoNSubstituteData]
        public void Sut_Should_Implement_IAccessTokenClient(AccessTokenClient sut)
        {
            Assert.IsAssignableFrom<IAccessTokenClient>(sut);
        }

        [Fact]
        public async Task Sut_Should_RequestAsync_Correctly_When_Sandbox()
        {
            var credentialsInBase64 = _fixture.Create<string>();

            var expectedRequestUri = "https://authsandbox.braspag.com.br/oauth2/token";

            var httpResponseMessage = new HttpResponseMessage();
            _sut.Configure().PostAsync(default, default).ReturnsForAnyArgs(httpResponseMessage);

            var expectedToken = _fixture.Create<AccessToken>();
            _httpResponseMessageHandler.HandleResponse<AccessToken>(default).ReturnsForAnyArgs(expectedToken);

            var result = await _sut.GetSandboxTokenAsync(credentialsInBase64);

            result.Should().BeEquivalentTo(expectedToken);

            await _sut.Received(1).PostAsync(expectedRequestUri, 
                Arg.Is<IDictionary<string, string>>(x => 
                    x.First().Key == "grant_type" &&
                    x.First().Value == "client_credentials"));

            await _sut.HttpResponseMessageHandler.Received(1).HandleResponse<AccessToken>(httpResponseMessage);
        }

        [Fact]
        public async Task Sut_Should_RequestAsync_Correctly_When_Production()
        {
            var credentialsInBase64 = _fixture.Create<string>();

            var expectedRequestUri = "https://auth.braspag.com.br/oauth2/token";

            var httpResponseMessage = new HttpResponseMessage();
            _sut.Configure().PostAsync(default, default).ReturnsForAnyArgs(httpResponseMessage);

            var expectedToken = _fixture.Create<AccessToken>();
            _httpResponseMessageHandler.HandleResponse<AccessToken>(default).ReturnsForAnyArgs(expectedToken);

            var result = await _sut.GetProductionTokenAsync(credentialsInBase64);

            result.Should().BeEquivalentTo(expectedToken);

            await _sut.Received(1).PostAsync(expectedRequestUri,
                Arg.Is<IDictionary<string, string>>(x =>
                    x.First().Key == "grant_type" &&
                    x.First().Value == "client_credentials"));

            await _sut.HttpResponseMessageHandler.Received(1).HandleResponse<AccessToken>(httpResponseMessage);
        }
    }
}
