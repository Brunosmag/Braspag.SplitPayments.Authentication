﻿using System;

namespace Braspag.Authentication.Domain.Services.Base64Encrypter
{
    public interface IBase64Encrypter
    {
        string EncryptInBase64(Guid clientId, Guid clientSecret);
    }
}
