using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Braspag.Authentication.Infrastructure.Handlers
{
    public class HttpResponseMessageHandler : IHttpResponseMessageHandler
    {
        public async Task<T> HandleResponse<T>(HttpResponseMessage httpResponseMessage)
        {
            var content = await ValidateContent(httpResponseMessage);

            var response = DeserializeJson<T>(content);

            return response;
        }

        private async Task<string> ValidateContent(HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            var isValid = httpResponseMessage.IsSuccessStatusCode;

            if (!isValid)
            {
                throw new HttpRequestException("An error occurred when calling an external dependency.");
            }

            return content;
        }

        private T DeserializeJson<T>(string content)
        {
            try
            {
                var response = JsonConvert.DeserializeObject<T>(content);

                return response;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
