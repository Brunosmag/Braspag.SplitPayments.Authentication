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

            var actual = await sut.GetProductionTokenAsync(clientCredentialsInBase64);

            actual.Should().Be(accessToken);

            await sut
                .AccessTokenClient
                .DidNotReceive()
                .GetProductionTokenAsync(clientCredentialsInBase64);
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

            var actual = await sut.GetSandboxTokenAsync(clientCredentialsInBase64);

            actual.Should().Be(accessToken);

            await sut
                .AccessTokenClient
                .DidNotReceive()
                .GetSandboxTokenAsync(clientCredentialsInBase64);
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_CreateProductionToken_Should_Get_Value_From_Client_When_Cache_Is_Empty(
            AccessToken accessToken,
            string clientCredentialsInBase64,
            AccessTokenClientWithCache sut)
        {
            sut
                .AccessTokenClient
                .GetProductionTokenAsync(clientCredentialsInBase64)
                .Returns(accessToken);

            var cacheKey = $"{clientCredentialsInBase64}-Production";

            sut.MemoryCache.TryGetValue(cacheKey, out object token).Returns(x =>
            {
                x[1] = null;
                return false;
            });

            var actual = await sut.GetProductionTokenAsync(clientCredentialsInBase64);

            actual.Should().Be(accessToken);

            await sut
                .AccessTokenClient
                .Received()
                .GetProductionTokenAsync(clientCredentialsInBase64);
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_CreateSandboxToken_Should_Get_Value_From_Client_When_Cache_Is_Empty(
            AccessToken accessToken,
            string clientCredentialsInBase64,
            AccessTokenClientWithCache sut)
        {
            sut
                .AccessTokenClient
                .GetSandboxTokenAsync(clientCredentialsInBase64)
                .Returns(accessToken);

            var cacheKey = $"{clientCredentialsInBase64}-Sandbox";

            sut.MemoryCache.TryGetValue(cacheKey, out object token).Returns(x =>
            {
                x[1] = null;
                return false;
            });

            var actual = await sut.GetSandboxTokenAsync(clientCredentialsInBase64);

            actual.Should().Be(accessToken);

            await sut
                .AccessTokenClient
                .Received()
                .GetSandboxTokenAsync(clientCredentialsInBase64);
        }
    }
}
