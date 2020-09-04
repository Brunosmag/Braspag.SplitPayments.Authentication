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

        public async Task<AccessToken> CreateProductionToken(string clientCredentialsInBase64)
        {
            var cacheKey = $"{clientCredentialsInBase64}-Production";

            if (MemoryCache.TryGetValue(cacheKey, out object token))
                return (AccessToken)token;

            token = await AccessTokenClient.CreateProductionToken(clientCredentialsInBase64);

            MemoryCache.Set(cacheKey, token, GetProductionCacheOptions());
            return (AccessToken)token;
        }

        public async Task<AccessToken> CreateSandboxToken(string clientCredentialsInBase64)
        {
            var cacheKey = $"{clientCredentialsInBase64}-Sandbox";

            if (MemoryCache.TryGetValue(cacheKey, out object token))
                return (AccessToken)token;

            token = await AccessTokenClient.CreateSandboxToken(clientCredentialsInBase64);

            MemoryCache.Set(cacheKey, token, GetSandboxCacheOptions());
            return (AccessToken)token;
        }

        private MemoryCacheEntryOptions GetProductionCacheOptions()
        {
            var productionCacheExpirationInSeconds = 1199;

            return new MemoryCacheEntryOptions()
                       .SetSlidingExpiration(TimeSpan.FromMinutes(productionCacheExpirationInSeconds));
        }

        private MemoryCacheEntryOptions GetSandboxCacheOptions()
        {
            var sandboxCacheExpirationInSeconds = 86399;

             return new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(sandboxCacheExpirationInSeconds));
        }
    }
}
