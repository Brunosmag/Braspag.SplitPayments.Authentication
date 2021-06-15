using Braspag.Authentication.Infrastructure.Contracts;
using System;
using System.Threading.Tasks;

namespace Braspag.Authentication.Domain.Services.BraspagTokenOrchestrator
{
    public interface IBraspagTokenOrchestrator
    {
        Task<AccessToken> CreateProductionTokenAsync(Guid clientId, string clientSecret);

        Task<AccessToken> CreateSandboxTokenAsync(Guid clientId, string clientSecret);
    }
}
