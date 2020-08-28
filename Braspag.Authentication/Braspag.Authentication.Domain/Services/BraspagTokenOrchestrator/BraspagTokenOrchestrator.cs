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


        public async Task<AccessToken> CreateProductionToken(Guid clientId, Guid clientSecret)
        {
            var clientCredentialsInBase64 = Base64Encrypter.EncryptInBase64(clientId, clientSecret);

            return await AccessTokenClient.CreateProductionToken(clientCredentialsInBase64);

        }

        public async Task<AccessToken> CreateSandboxToken(Guid clientId, Guid clientSecret)
        {
            var clientCredentialsInBase64 = Base64Encrypter.EncryptInBase64(clientId, clientSecret);

            return await AccessTokenClient.CreateSandboxToken(clientCredentialsInBase64);
        }
    }
}
