using Braspag.Authentication.Infrastructure.Contracts;
using System.Threading.Tasks;

namespace Braspag.Authentication.Infrastructure.Clients
{
    public interface IAccessTokenClient
    {
        Task<AccessToken> GetSandboxTokenAsync(string clientCredentialsInBase64);

        Task<AccessToken> GetProductionTokenAsync(string clientCredentialsInBase64);
    }
}
