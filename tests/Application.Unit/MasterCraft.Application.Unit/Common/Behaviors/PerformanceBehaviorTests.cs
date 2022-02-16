using NUnit.Framework;
using MasterCraft.Application.Common.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Logging;
using MasterCraft.Application.Authentication.Commands.GenerateToken;
using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Core.ReportModels;
using MediatR;
using System.Threading;

namespace MasterCraft.Application.Common.Behaviors.Tests
{
    [TestFixture()]
    public class PerformanceBehaviorTests
    {
        private Mock<ILogger<GenerateTokenCommand>> cLogger = null!;
        private Mock<ICurrentUserService> cCurrentUserService = null!;
        private Mock<IIdentityService> cIdentityService = null!;
        private Mock<RequestHandlerDelegate<AccessTokenReportModel>> cNextDelegate;
        private GenerateTokenCommand cCommand;

        [SetUp]
        public void Setup()
        {
            cLogger = new Mock<ILogger<GenerateTokenCommand>>();
            cCurrentUserService = new Mock<ICurrentUserService>();
            cIdentityService = new Mock<IIdentityService>();
            cNextDelegate = new();
            cCommand = new();
        }

        [Test]
        public async Task ShouldLogWarningIfOver500Millis()
        {
            cNextDelegate.Setup(del => del()).Returns(() =>
            {
                Thread.Sleep(500);
                return Task.FromResult(new AccessTokenReportModel());
            });

            PerformanceBehavior<GenerateTokenCommand, AccessTokenReportModel> lBehavior = new(cLogger.Object, cCurrentUserService.Object, cIdentityService.Object);
            await lBehavior.Handle(cCommand, new CancellationToken(), cNextDelegate.Object);

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
            cNextDelegate.Setup(del => del()).Returns(() =>
            {
                return Task.FromResult(new AccessTokenReportModel());
            });

            PerformanceBehavior<GenerateTokenCommand, AccessTokenReportModel> lBehavior = new(cLogger.Object, cCurrentUserService.Object, cIdentityService.Object);
            await lBehavior.Handle(cCommand, new CancellationToken(), cNextDelegate.Object);

            cLogger.Verify(logger => logger.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Never);
        }
    }
}