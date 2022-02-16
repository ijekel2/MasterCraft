using NUnit.Framework;
using MasterCraft.Application.Common.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Threading;
using Microsoft.Extensions.Logging;
using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Application.Authentication.Commands.GenerateToken;
using MasterCraft.Core.ReportModels;

namespace MasterCraft.Application.Common.Behaviors.Tests
{
    [TestFixture()]
    public class LoggingBehaviorTests
    {
        private Mock<ILogger<GenerateTokenCommand>> cLogger = null!;
        private Mock<ICurrentUserService> cCurrentUserService = null!;
        private Mock<IIdentityService> cIdentityService = null!;

        [SetUp]
        public void Setup()
        {
            cLogger = new Mock<ILogger<GenerateTokenCommand>>();
            cCurrentUserService = new Mock<ICurrentUserService>();
            cIdentityService = new Mock<IIdentityService>();
        }

        [Test]
        public async Task ShouldLogInformation()
        {
            var requestLogger = new LoggingBehavior<GenerateTokenCommand>(cLogger.Object, cCurrentUserService.Object, cIdentityService.Object);

            await requestLogger.Process(new GenerateTokenCommand { Username = "username", Password = "password" }, new CancellationToken());

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

            var requestLogger = new LoggingBehavior<GenerateTokenCommand>(cLogger.Object, cCurrentUserService.Object, cIdentityService.Object);

            await requestLogger.Process(new GenerateTokenCommand { Username = "username", Password = "password" }, new CancellationToken());

            cIdentityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task ShouldNotCallGetUserNameAsyncOnceIfUnauthenticated()
        {
            var requestLogger = new LoggingBehavior<GenerateTokenCommand>(cLogger.Object, cCurrentUserService.Object, cIdentityService.Object);

            await requestLogger.Process(new GenerateTokenCommand { Username = "username", Password = "password" }, new CancellationToken());

            cIdentityService.Verify(i => i.GetUserNameAsync(It.IsAny<string>()), Times.Never);
        }
    }
}