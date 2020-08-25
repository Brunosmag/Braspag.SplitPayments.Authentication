using Braspag.Authentication.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace Braspag.Authentication.Domain.Services.BraspagTokenOrchestrator
{
    public interface IBraspagTokenOrchestrator
    {
        Task<AccessToken> CreateProductionToken(Guid clientId, Guid clientSecret);

        Task<AccessToken> CreateSandboxToken(Guid clientId, Guid clientSecret);
    }
}
