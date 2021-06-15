using Braspag.Authentication.Domain.Services.Base64Encrypters;
using Braspag.Authentication.Infrastructure.Clients;
using Braspag.Authentication.Infrastructure.Contracts;
using System;
using System.Threading.Tasks;

namespace Braspag.Authentication.Domain.Services.BraspagTokenOrchestrator
{
    public class BraspagTokenOrchestrator : IBraspagTokenOrchestrator
    {
        public IBase64Encoder Base64Encoder { get; }
        public IAccessTokenClient AccessTokenClient { get; }

        public BraspagTokenOrchestrator(
            IBase64Encoder base64Encoder,
            IAccessTokenClient accessTokenClient)
        {
            Base64Encoder = base64Encoder ?? throw new ArgumentNullException(nameof(base64Encoder));
            AccessTokenClient = accessTokenClient ?? throw new ArgumentNullException(nameof(accessTokenClient));
        }

        public async Task<AccessToken> CreateProductionTokenAsync(Guid clientId, string clientSecret)
        {
            string productionCredentialsInBase64 = EncodeCredentialsInBase64(clientId, clientSecret);

            return await AccessTokenClient.GetProductionTokenAsync(productionCredentialsInBase64);
        }

        public async Task<AccessToken> CreateSandboxTokenAsync(Guid clientId, string clientSecret)
        {
            var sandboxCredentialsInBase64 = EncodeCredentialsInBase64(clientId, clientSecret);

            return await AccessTokenClient.GetSandboxTokenAsync(sandboxCredentialsInBase64);
        }

        private string EncodeCredentialsInBase64(Guid clientId, string clientSecret)
            => Base64Encoder.EncodeInBase64(clientId, clientSecret);
    }
}
