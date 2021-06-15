using Newtonsoft.Json;

namespace Braspag.Authentication.Infrastructure.Contracts
{
    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("token_type")]
        public string Type { get; set; }

        [JsonProperty("expires_in")]
        public double ExpiresIn { get; set; }
    }
}
