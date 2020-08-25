using Braspag.Authentication.Domain.Contracts;
using Braspag.Authentication.Domain.Services.Base64Encrypters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Braspag.Authentication.Domain.Services.BraspagTokenOrchestrator
{
    public class BraspagTokenOrchestrator : IBraspagTokenOrchestrator
    {
        public IBase64Encrypter Base64Encrypter { get; }
        public IHttpClientFactory HttpClientFactory { get; }
        public IConfiguration Configuration { get; }

        public BraspagTokenOrchestrator(
            IBase64Encrypter base64Encrypter,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            Base64Encrypter = base64Encrypter ?? throw new ArgumentNullException(nameof(base64Encrypter));
            HttpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        public async Task<AccessToken> CreateProductionToken(Guid clientId, Guid clientSecret)
        {
            return await RequestAccessToken(clientId, clientSecret, "");

            throw new NotImplementedException();

        }

        public async Task<AccessToken> CreateSandboxToken(Guid clientId, Guid clientSecret)
        {
            return await RequestAccessToken(clientId, clientSecret, "");

            throw new NotImplementedException();
        }

        private async Task<AccessToken> RequestAccessToken(Guid clientId, Guid clientSecret, string endpoint)
        {
            var base64Hash = Base64Encrypter.EncryptInBase64(clientId, clientSecret);

            var httpClient = HttpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(endpoint);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Hash);

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
