using MasterCraft.Domain.Authentication;
using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.Common.RequestHandling;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.UnitTests;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Common.Tests
{
    [TestFixture()]
    public class RequestHandlerTest : TestBase
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

            cIdentityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }       

        [Test]
        public async Task ShouldLogError()
        {
            try
            {
                GenerateToken handler = new GenerateToken(cHandlerService);
                await handler.HandleRequest(null);
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
            cIdentityService.Setup(service => service.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(() =>
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
            GenerateToken handler = new GenerateToken(cHandlerService);
            await handler.HandleRequest(new GenerateTokenRequest { Username = TestConstants.TestMentor.Username, Password = "mentor!123" });
        }
    }
}