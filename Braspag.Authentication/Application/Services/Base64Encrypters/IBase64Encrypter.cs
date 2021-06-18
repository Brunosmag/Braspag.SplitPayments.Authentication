using System;

namespace Braspag.Authentication.Application.Services.Base64Encrypters
{
    public interface IBase64Encoder
    {
        string EncodeInBase64(Guid clientId, string clientSecret);
    }
}
