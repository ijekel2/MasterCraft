using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Threading;
using Microsoft.Extensions.Logging;
using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Application.Authentication.GenerateToken;
using MasterCraft.Core.Reports;
using MasterCraft.Core.Requests;
using MasterCraft.Application.Common.Utilities;

namespace MasterCraft.Application.Common.Tests
{
    [TestFixture()]
    public class MediatorTests
    {
        private Mock<ILogger<Mediator>> cLogger = null!;
        private Mock<ICurrentUserService> cCurrentUserService = null!;
        private Mock<IIdentityService> cIdentityService = null!;

        [SetUp]
        public void Setup()
        {
            cLogger = new Mock<ILogger<Mediator>>();
            cCurrentUserService = new Mock<ICurrentUserService>();
            cIdentityService = new Mock<IIdentityService>();
        }

        [Test]
        public async Task ShouldLogInformation()
        {
            await SendTestRequest();

            cLogger.Verify(logger => logger.Log(
                It.Is<LogLevel>(level => level == LogLevel.Information),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }

        [Test]
        public async Task ShouldCallGetUserNameAsyncOnceIfAuthenticated()
        {
            cCurrentUserService.Setup(x => x.UserId).Returns(Guid.NewGuid().ToString());

            await SendTestRequest();

            cIdentityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            await SendTestRequest();

            cIdentityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task ShouldLogError()
        {
            try
            {
                var mediator = new Mediator(cLogger.Object, cCurrentUserService.Object, cIdentityService.Object);

                await mediator.Send<GenerateTokenRequest, AccessTokenReport>(
                    new GenerateTokenRequest { Username = "username", Password = "password" },
                    null);
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
            var mediator = new Mediator(cLogger.Object, cCurrentUserService.Object, cIdentityService.Object);
            Mock<GenerateTokenHandler> handler = new Mock<GenerateTokenHandler>(cIdentityService.Object);
            handler.Setup(handler => handler.Validate(It.IsAny<GenerateTokenRequest>(), It.IsAny<Validator>())).Returns(() => Task.FromResult(new Validator()));
            handler.Setup(handler => handler.Handle(It.IsAny<GenerateTokenRequest>())).Returns(() =>
            {
                Thread.Sleep(500);
                return Task.FromResult(new AccessTokenReport());
            });

            await mediator.Send(
                new GenerateTokenRequest { Username = "username", Password = "password" },
                handler.Object);

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
            var mediator = new Mediator(cLogger.Object, cCurrentUserService.Object, cIdentityService.Object);

            await mediator.Send(
                new GenerateTokenRequest { Username = "username", Password = "password" },
                new GenerateTokenHandler(cIdentityService.Object));
        }
    }
}