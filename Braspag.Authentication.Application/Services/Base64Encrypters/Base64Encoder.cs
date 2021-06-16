using System;
using System.Text;

namespace Braspag.Authentication.Application.Services.Base64Encrypters
{
    public class Base64Encoder : IBase64Encoder
    {
        public string EncodeInBase64(Guid clientId, string clientSecret)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        }
    }
}
