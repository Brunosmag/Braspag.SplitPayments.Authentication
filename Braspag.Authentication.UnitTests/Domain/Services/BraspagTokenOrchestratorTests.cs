using AutoFixture.Idioms;
using Braspag.Authentication.UnitTests.AutoFixture;
using System;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using Braspag.Authentication.Infrastructure.Contracts;
using FluentAssertions;
using Braspag.Authentication.Application.Services.BraspagTokenOrchestrator;

namespace Braspag.Authentication.UnitTests.Domain.Services
{
    public class BraspagTokenOrchestratorTests
    {
        [Theory,AutoNSubstituteData]
        public void Sut_Should_Guard_Its_Clause(GuardClauseAssertion assertion)
        {
            assertion.Verify(typeof(BraspagTokenOrchestrator).GetConstructors());
        }

        [Theory,AutoNSubstituteData]
        public void Sut_Should_Implement_IBraspagTokenOrchestrator(BraspagTokenOrchestrator sut)
        {
            Assert.IsAssignableFrom<IBraspagTokenOrchestrator>(sut);
        }

        [Theory,AutoNSubstituteData]
        public async Task Sut_Should_Create_Sandbox_Token_With_Correct_Base64Encode(
            Guid clientId,
            string clientSecret,
            string base64Encode,
            AccessToken accessToken,
            BraspagTokenOrchestrator sut)
        {
            sut.Base64Encoder.EncodeInBase64(clientId, clientSecret).Returns(base64Encode);
            sut.AccessTokenClient.GetSandboxTokenAsync(base64Encode).Returns(accessToken);

            var actual = await sut.CreateSandboxTokenAsync(clientId, clientSecret);

            await sut.AccessTokenClient.Received().GetSandboxTokenAsync(base64Encode);

            actual.Should().Be(accessToken);
        }

        [Theory, AutoNSubstituteData]
        public async Task Sut_Should_Create_Production_Token_With_Correct_Base64Encode(
           Guid clientId,
           string clientSecret,
           string base64Encode,
           AccessToken accessToken,
           BraspagTokenOrchestrator sut)
        {
            sut.Base64Encoder.EncodeInBase64(clientId, clientSecret).Returns(base64Encode);
            sut.AccessTokenClient.GetProductionTokenAsync(base64Encode).Returns(accessToken);

            var actual = await sut.CreateProductionTokenAsync(clientId, clientSecret);

            await sut.AccessTokenClient.Received().GetProductionTokenAsync(base64Encode);

            actual.Should().Be(accessToken);
        }
    }
}
