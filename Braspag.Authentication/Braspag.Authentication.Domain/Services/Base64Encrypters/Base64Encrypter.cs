using System;
using System.Text;

namespace Braspag.Authentication.Domain.Services.Base64Encrypters
{
    public class Base64Encrypter : IBase64Encrypter
    {
        public string EncryptInBase64(Guid clientId, string clientSecret)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
        }
    }
}
