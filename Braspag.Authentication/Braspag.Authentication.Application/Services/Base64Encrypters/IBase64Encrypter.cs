using System;

namespace Braspag.Authentication.Domain.Services.Base64Encrypters
{
    public interface IBase64Encoder
    {
        string EncodeInBase64(Guid clientId, string clientSecret);
    }
}
