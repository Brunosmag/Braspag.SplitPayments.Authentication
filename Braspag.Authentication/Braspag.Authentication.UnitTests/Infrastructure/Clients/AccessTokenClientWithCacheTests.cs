using AutoFixture.Idioms;
using Braspag.Authentication.Infrastructure.Clients;
using Braspag.Authentication.Infrastructure.Contracts;
using Braspag.Authentication.UnitTests.AutoFixture;
using FluentAssertions;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Braspag.Authentication.UnitTests.Infrastructure.Clients
{
    public class AccessTokenClientWithCacheTests
    {
        [Theory,AutoNSubstituteData]
        public void Sut_Should_Guard_Its_Clause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(AccessTokenClientWithCache).GetConstructors());
        }

        [Theory,AutoNSubstituteData]
        public void Sut_Should_Implement_IAccessTokenClient(AccessTokenClientWithCache sut)
        {
            Assert.IsAssignableFrom<IAccessTokenClient>(sut);
        }

        [Theory,AutoNSubstituteData]
        public async Task Sut_CreateProductionToken_Should_Get_Value_From_Cache_When_There_Was_Value(
            AccessToken accessToken,
            string clientCredentialsInBase64,
            AccessTokenClientWithCache sut)
        {
            var cacheKey = $"{clientCredentialsInBase64}-Production";

            sut.MemoryCache.TryGetValue(cacheKey, out object token).Returns(x =>
            {
                x[1] = accessToken;
                return true;
            });

            var actual = await sut.CreateProductionToken(clientCredentialsInBase64);

            actual.Should().Be(accessToken);

            await sut
                .AccessTokenClient
                .DidNotReceive()
                .CreateProductionToken(clientCredentialsInBase64);
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_CreateSandboxToken_Should_Get_Value_From_Cache_When_There_Was_Value(
            AccessToken accessToken,
            string clientCredentialsInBase64,
            AccessTokenClientWithCache sut)
        {
            var cacheKey = $"{clientCredentialsInBase64}-Sandbox";

            sut.MemoryCache.TryGetValue(cacheKey, out object token).Returns(x =>
            {
                x[1] = accessToken;
                return true;
            });

            var actual = await sut.CreateSandboxToken(clientCredentialsInBase64);

            actual.Should().Be(accessToken);

            await sut
                .AccessTokenClient
                .DidNotReceive()
                .CreateSandboxToken(clientCredentialsInBase64);
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_CreateProductionToken_Should_Get_Value_From_Client_When_Cache_Is_Empty(
            AccessToken accessToken,
            string clientCredentialsInBase64,
            AccessTokenClientWithCache sut)
        {
            sut
                .AccessTokenClient
                .CreateProductionToken(clientCredentialsInBase64)
                .Returns(accessToken);

            var cacheKey = $"{clientCredentialsInBase64}-Production";

            sut.MemoryCache.TryGetValue(cacheKey, out object token).Returns(x =>
            {
                x[1] = null;
                return false;
            });

            var actual = await sut.CreateProductionToken(clientCredentialsInBase64);

            actual.Should().Be(accessToken);

            await sut
                .AccessTokenClient
                .Received()
                .CreateProductionToken(clientCredentialsInBase64);
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_CreateSandboxToken_Should_Get_Value_From_Client_When_Cache_Is_Empty(
            AccessToken accessToken,
            string clientCredentialsInBase64,
            AccessTokenClientWithCache sut)
        {
            sut
                .AccessTokenClient
                .CreateSandboxToken(clientCredentialsInBase64)
                .Returns(accessToken);

            var cacheKey = $"{clientCredentialsInBase64}-Sandbox";

            sut.MemoryCache.TryGetValue(cacheKey, out object token).Returns(x =>
            {
                x[1] = null;
                return false;
            });

            var actual = await sut.CreateSandboxToken(clientCredentialsInBase64);

            actual.Should().Be(accessToken);

            await sut
                .AccessTokenClient
                .Received()
                .CreateSandboxToken(clientCredentialsInBase64);
        }
    }
}
