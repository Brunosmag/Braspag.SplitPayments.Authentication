using Braspag.Authentication.Infrastructure.Contracts;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Braspag.Authentication.Infrastructure.Clients
{
    public class AccessTokenClientWithCache : IAccessTokenClient
    {
        public IMemoryCache MemoryCache { get; }
        public IAccessTokenClient AccessTokenClient { get; }

        public AccessTokenClientWithCache(
            IMemoryCache memoryCache,
            IAccessTokenClient accessTokenClient)
        {
            MemoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            AccessTokenClient = accessTokenClient ?? throw new ArgumentNullException(nameof(accessTokenClient));
        }

        public async Task<AccessToken> GetProductionTokenAsync(string clientCredentialsInBase64)
        {
            var cacheKey = $"{clientCredentialsInBase64}-Production";

            if (MemoryCache.TryGetValue(cacheKey, out AccessToken token))
                return token;

            token = await AccessTokenClient.GetProductionTokenAsync(clientCredentialsInBase64);

            MemoryCache.Set(cacheKey, token, GetCacheOptions(token));

            return token;
        }

        public async Task<AccessToken> GetSandboxTokenAsync(string clientCredentialsInBase64)
        {
            var cacheKey = $"{clientCredentialsInBase64}-Sandbox";

            if (MemoryCache.TryGetValue(cacheKey, out AccessToken token))
                return token;

            token = await AccessTokenClient.GetSandboxTokenAsync(clientCredentialsInBase64);

            MemoryCache.Set(cacheKey, token, GetCacheOptions(token));

            return token;
        }

        private MemoryCacheEntryOptions GetCacheOptions(AccessToken accessToken)
        {
            var defaultTolerance = 20;
            var cacheDurationInSeconds = accessToken.ExpiresIn - defaultTolerance;

            return new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(cacheDurationInSeconds));
        }

    }
}
