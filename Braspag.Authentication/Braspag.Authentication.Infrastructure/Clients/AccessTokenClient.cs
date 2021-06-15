using Braspag.Authentication.Infrastructure.Contracts;
using Braspag.Authentication.Infrastructure.Handlers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Braspag.Authentication.Infrastructure.Clients
{
    public class AccessTokenClient : IAccessTokenClient
    {
        public HttpClient HttpClientFactory { get; }
        public IHttpResponseMessageHandler HttpResponseMessageHandler { get; }

        public AccessTokenClient(
            HttpClient httpClientFactory,
            IHttpResponseMessageHandler httpResponseMessageHandler)
        {
            HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            HttpResponseMessageHandler = httpResponseMessageHandler ?? throw new ArgumentNullException(nameof(httpResponseMessageHandler));
        }


        public async Task<AccessToken> GetProductionTokenAsync(string clientCredentialsInBase64)
        {
            const string requestUri = "https://auth.braspag.com.br/oauth2/token";

            var accessToken = await RequestAccessTokenAsync(clientCredentialsInBase64, requestUri);

            return accessToken;
        }

        public async Task<AccessToken> GetSandboxTokenAsync(string clientCredentialsInBase64)
        {
            const string requestUri = "https://authsandbox.braspag.com.br/oauth2/token";

            var accessToken = await RequestAccessTokenAsync(clientCredentialsInBase64, requestUri);

            return accessToken;
        }

        private async Task<AccessToken> RequestAccessTokenAsync(string clientCredentialsInBase64, string requestUri)
        {
            HttpClientFactory.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", clientCredentialsInBase64);

            var content = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            };

            var response = await PostAsync(requestUri, content);

            return await HttpResponseMessageHandler.HandleResponse<AccessToken>(response);
        }

        public virtual async Task<HttpResponseMessage> PostAsync(string requestUri, IDictionary<string, string> content)
        {
            var response = await HttpClientFactory.PostAsync(requestUri, new FormUrlEncodedContent(content));
            return response;
        }
    }
}
