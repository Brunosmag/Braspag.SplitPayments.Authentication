using Braspag.Authentication.UnitTests.AutoFixture;
using System;
using Xunit;
using System.Text;
using Braspag.Authentication.Domain.Services.Base64Encrypters;
using FluentAssertions;

namespace Braspag.Authentication.UnitTests.Domain.Services.Base64Encrypters
{
    public class Base64EncrypterTests
    {
        [Theory,AutoNSubstituteData]
        public void Sut_Should_Implement_IBase64Encrypter(
            Base64Encrypter sut)
        {
            Assert.IsAssignableFrom<IBase64Encrypter>(sut);
        }

        [Theory,AutoNSubstituteData]
        public void Sut_Should_Correctly_Generate_Base64_Hash_With_ClientId_And_ClientSecret(
            Guid clientId,
            Guid clientSecret,
            Base64Encrypter sut)
        {
            var expected = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            var actual = sut.EncryptInBase64(clientId, clientSecret);

            actual.Should().Be(expected);
        }
    }
}
