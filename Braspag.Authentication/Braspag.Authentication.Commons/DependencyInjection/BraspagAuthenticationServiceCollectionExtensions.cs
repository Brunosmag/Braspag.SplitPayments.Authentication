using Braspag.Authentication.Domain.Services.Base64Encrypters;
using Braspag.Authentication.Domain.Services.BraspagTokenOrchestrator;
using Braspag.Authentication.Infrastructure.Clients;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Braspag.Authentication.Commons.DependencyInjection
{
    public static class BraspagAuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddBraspagAuthentication(this IServiceCollection services)
        {
            services.AddTransient<IBase64Encrypter, Base64Encrypter>();

            services.AddTransient<IBraspagTokenOrchestrator, BraspagTokenOrchestrator>();

            services.AddTransient<IAccessTokenClient>(resolver => 
            {
                return new AccessTokenClientWithCache(
                    resolver.GetService<IMemoryCache>(),
                    new AccessTokenClient(resolver.GetService<IHttpClientFactory>()));
            });

            return services;
        }
    }
}
