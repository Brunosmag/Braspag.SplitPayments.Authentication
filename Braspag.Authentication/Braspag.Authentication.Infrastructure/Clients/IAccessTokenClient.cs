using Braspag.Authentication.Infrastructure.Contracts;
using System;
using System.Threading.Tasks;

namespace Braspag.Authentication.Infrastructure.Clients
{
    public interface IAccessTokenClient
    {
        Task<AccessToken> CreateSandboxToken(string clientCredentialsInBase64);

        Task<AccessToken> CreateProductionToken(string clientCredentialsInBase64);
    }
}
