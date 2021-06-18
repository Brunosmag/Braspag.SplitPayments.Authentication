using Braspag.Authentication.Infrastructure.Clients;
using Braspag.Authentication.Infrastructure.Handlers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Braspag.Authentication.Application.Services.Base64Encrypters;
using Braspag.Authentication.Application.Services.BraspagTokenOrchestrator;

namespace Braspag.Authentication.DependencyInjection
{
    public static class BraspagAuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddBraspagAuthentication(this IServiceCollection services)
        {
            services.AddTransient<IBase64Encrypter, Base64Encoder>();

            services.AddTransient<IBraspagTokenOrchestrator, BraspagTokenOrchestrator>();

            services.AddTransient<IHttpResponseMessageHandler, HttpResponseMessageHandler>();

            services.AddTransient<IAccessTokenClient>(resolver => 
            {
                return new AccessTokenClientWithCache(
                    resolver.GetService<IMemoryCache>(),
                    new AccessTokenClient(
                        resolver.GetService<IHttpClientFactory>().CreateClient(),
                        resolver.GetService<IHttpResponseMessageHandler>()));
            });

            return services;
        }
    }
}
