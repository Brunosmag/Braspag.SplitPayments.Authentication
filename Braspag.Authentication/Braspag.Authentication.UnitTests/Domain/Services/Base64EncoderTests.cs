using Braspag.Authentication.UnitTests.AutoFixture;
using System;
using Xunit;
using System.Text;
using Braspag.Authentication.Domain.Services.Base64Encrypters;
using FluentAssertions;

namespace Braspag.Authentication.UnitTests.Domain.Services
{
    public class Base64EncoderTests
    {
        [Theory,AutoNSubstituteData]
        public void Sut_Should_Implement_IBase64Encrypter(
            Base64Encoder sut)
        {
            Assert.IsAssignableFrom<IBase64Encoder>(sut);
        }

        [Theory,AutoNSubstituteData]
        public void Sut_Should_Correctly_Generate_Base64_Hash_With_ClientId_And_ClientSecret(
            Guid clientId,
            string clientSecret,
            Base64Encoder sut)
        {
            var expected = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

            var actual = sut.EncodeInBase64(clientId, clientSecret);

            actual.Should().Be(expected);
        }
    }
}
