using System.Net.Http;
using System.Threading.Tasks;

namespace Braspag.Authentication.Infrastructure.Handlers
{
    public interface IHttpResponseMessageHandler
    {
        Task<T> HandleResponse<T>(HttpResponseMessage content);
    }
}
