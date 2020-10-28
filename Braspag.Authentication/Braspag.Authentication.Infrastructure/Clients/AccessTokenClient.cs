using Braspag.Authentication.Infrastructure.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Braspag.Authentication.Infrastructure.Clients
{
    public class AccessTokenClient : IAccessTokenClient
    {
        public IHttpClientFactory HttpClientFactory { get; }

        public AccessTokenClient(
            IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }


        public async Task<AccessToken> GetProductionTokenAsync(string clientCredentialsInBase64)
        {
            const string endpoint = "https://auth.braspag.com.br";

            var accessToken = await RequestAccessTokenAsync(clientCredentialsInBase64, endpoint);

            return accessToken;
        }

        public async Task<AccessToken> GetSandboxTokenAsync(string clientCredentialsInBase64)
        {
            const string endpoint = "https://authsandbox.braspag.com.br";

            var accessToken = await RequestAccessTokenAsync(clientCredentialsInBase64, endpoint);

            return accessToken;
        }

        private async Task<AccessToken> RequestAccessTokenAsync(string clientCredentialsInBase64, string endpoint)
        {
            var httpClient = HttpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(endpoint);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", clientCredentialsInBase64);

            var content = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            };

            var response = await httpClient.PostAsync("/oauth2/token", new FormUrlEncodedContent(content));

            var stringResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<AccessToken>(stringResponse);
        }
    }
}
