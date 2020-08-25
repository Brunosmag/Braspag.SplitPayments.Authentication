using Braspag.Authentication.Domain.Services.Base64Encrypters;
using Microsoft.Extensions.DependencyInjection;

namespace Braspag.Authentication.Commons.DependencyInjection
{
    public static class BraspagAuthenticationCollectionExtensions
    {
        public static void AddBraspagAuthentication(this IServiceCollection services)
        {
            services.AddHttpClient();

            services.AddTransient<IBase64Encrypter, Base64Encrypter>();
        }
    }
}
