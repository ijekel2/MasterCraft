using NUnit.Framework;
using MasterCraft.Application.Common.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Moq;
using Microsoft.Extensions.Logging;
using MasterCraft.Application.Authentication.Commands.GenerateToken;
using MasterCraft.Core.ReportModels;
using System.Threading.Tasks;

namespace MasterCraft.Application.Common.Behaviors.Tests
{
    [TestFixture()]
    public class UnhandledExceptionBehaviorTests
    {
        private Mock<ILogger<GenerateTokenCommand>> cLogger = null!;
        private GenerateTokenCommand cCommand;

        [SetUp]
        public void Setup()
        {
            cLogger = new Mock<ILogger<GenerateTokenCommand>>();
            cCommand = new();
        }

        [Test]
        public void ShouldThrowException()
        {
            UnhandledExceptionBehavior<GenerateTokenCommand, AccessTokenReportModel> lBehavior = new(cLogger.Object);
            Assert.ThrowsAsync<NullReferenceException>(() => lBehavior.Handle(cCommand, new CancellationToken(), null));
        }

        [Test]
        public async Task ShouldLogError()
        {
            try
            {
                UnhandledExceptionBehavior<GenerateTokenCommand, AccessTokenReportModel> lBehavior = new(cLogger.Object);
                await lBehavior.Handle(cCommand, new CancellationToken(), null);
            }
            catch { }

            cLogger.Verify(logger => logger.Log(
                It.Is<LogLevel>(level => level == LogLevel.Error),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => true),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Once);
        }
    }
}