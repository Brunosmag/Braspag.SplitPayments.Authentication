using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Braspag.Authentication.UnitTests.NSubstitute
{
    public class FakeHttpMessageWrapper : HttpMessageHandler
    {
        public HttpContent HttpContent { get; set; }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = HttpContent });
        }
    }
}
