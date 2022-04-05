using MasterCraft.Domain.Services.Authentication;
using MasterCraft.Domain.UnitTests;
using MasterCraft.Shared.ViewModels;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Tests
{
    [TestFixture()]
    public class DomainServiceTests : TestBase
    {
        [Test]
        public async Task ShouldLogInformation()
        {
            AuthenticateUser(TestConstants.TestMentor);

            await SendTestRequest();

            cLogger.Verify(logger => logger.Log(
                It.Is<LogLevel>(level => level == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        [Test]
        public async Task ShouldCallGetUserNameAsyncOnce()
        {
            AuthenticateUser(TestConstants.TestMentor);

            await SendTestRequest();

            cIdentityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        }       

        [Test]
        public async Task ShouldLogError()
        {
            try
            {
                GenerateTokenService service = new (cHandlerService);
                await service.HandleRequest(null);
            }
            catch { }

            cLogger.Verify(logger => logger.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        [Test]
        public async Task ShouldLogWarningIfOver500Millis()
        {
            AuthenticateUser(TestConstants.TestMentor);
           
            //-- Setup this method to take longer so we trip the performance logging.
            cIdentityService.Setup(service => service.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(() =>
            {
                Thread.Sleep(1000);
                return Task.FromResult(true);
            });

            await SendTestRequest();

            cLogger.Verify(logger => logger.Log(
                 It.Is<LogLevel>(level => level == LogLevel.Warning),
                 It.IsAny<EventId>(),
                 It.Is<It.IsAnyType>((v, t) => true),
                 It.IsAny<Exception>(),
                 It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        [Test]
        public async Task ShouldDoNothingIfUnder500Millis()
        {
            AuthenticateUser(TestConstants.TestMentor);

            await SendTestRequest();

            cLogger.Verify(logger => logger.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Never);
        }

        private async Task SendTestRequest()
        {
            GenerateTokenService service = new(cHandlerService);
            await service.HandleRequest(new GenerateTokenViewModel { Username = TestConstants.TestMentor.Username, Password = "mentor!123" });
        }
    }
}