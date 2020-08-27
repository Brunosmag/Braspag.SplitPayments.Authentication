using Braspag.Authentication.Domain.Services.Base64Encrypters;
using Braspag.Authentication.Domain.Services.BraspagTokenOrchestrator;
using Microsoft.Extensions.DependencyInjection;

namespace Braspag.Authentication.Commons.DependencyInjection
{
    public static class BraspagAuthenticationCollectionExtensions
    {
        public static IServiceCollection AddBraspagAuthentication(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<IBase64Encrypter, Base64Encrypter>();

            services.AddTransient<IBraspagTokenOrchestrator, BraspagTokenOrchestrator>();

            return services;
        }
    }
}
