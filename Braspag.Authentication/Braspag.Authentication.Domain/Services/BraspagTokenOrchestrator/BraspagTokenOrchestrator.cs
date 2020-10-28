using Braspag.Authentication.Domain.Services.Base64Encrypters;
using Braspag.Authentication.Infrastructure.Clients;
using Braspag.Authentication.Infrastructure.Contracts;
using System;
using System.Threading.Tasks;

namespace Braspag.Authentication.Domain.Services.BraspagTokenOrchestrator
{
    public class BraspagTokenOrchestrator : IBraspagTokenOrchestrator
    {
        public IBase64Encrypter Base64Encrypter { get; }
        public IAccessTokenClient AccessTokenClient { get; }

        public BraspagTokenOrchestrator(
            IBase64Encrypter base64Encrypter,
            IAccessTokenClient accessTokenClient)
        {
            Base64Encrypter = base64Encrypter ?? throw new ArgumentNullException(nameof(base64Encrypter));
            AccessTokenClient = accessTokenClient ?? throw new ArgumentNullException(nameof(accessTokenClient));
        }

        public async Task<AccessToken> CreateProductionToken(Guid clientId, string clientSecret)
        {
            string productionCredentialsInBase64 = EncryptCredentialsInBase64(clientId, clientSecret);

            return await AccessTokenClient.CreateProductionToken(productionCredentialsInBase64);

        }

        public async Task<AccessToken> CreateSandboxToken(Guid clientId, string clientSecret)
        {
            var sandboxCredentialsInBase64 = EncryptCredentialsInBase64(clientId, clientSecret);

            return await AccessTokenClient.CreateSandboxToken(sandboxCredentialsInBase64);
        }

        private string EncryptCredentialsInBase64(Guid clientId, string clientSecret)
            => Base64Encrypter.EncryptInBase64(clientId, clientSecret);
    }
}
