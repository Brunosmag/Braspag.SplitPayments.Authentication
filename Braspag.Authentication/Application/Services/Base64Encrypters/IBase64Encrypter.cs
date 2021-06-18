using System;

namespace Braspag.Authentication.Application.Services.Base64Encrypters
{
    public interface IBase64Encrypter
    {
        string EncodeInBase64(Guid clientId, string clientSecret);
    }
}
