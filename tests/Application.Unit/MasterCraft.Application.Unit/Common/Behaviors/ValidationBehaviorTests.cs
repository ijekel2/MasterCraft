using NUnit.Framework;
using MasterCraft.Application.Common.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterCraft.Application.Authentication.Commands.GenerateToken;
using MasterCraft.Core.CommandModels;
using MasterCraft.Core.ReportModels;
using MasterCraft.Application.Common.Interfaces;
using Moq;
using MasterCraft.Application.Common.Exceptions;
using System.Threading;
using MediatR;
using FluentValidation;
using FluentValidation.Results;
using ValidationException = MasterCraft.Application.Common.Exceptions.ValidationException;

namespace MasterCraft.Application.Common.Behaviors.Tests
{
    [TestFixture()]
    public class ValidationBehaviorTests
    {
        private Mock<IIdentityService> cIdentityService = null!;
        private GenerateTokenCommand cCommand;

        [SetUp]
        public void Setup()
        {
            cIdentityService = new Mock<IIdentityService>();
            cCommand = new();
        }

        [Test]
        public void FailedValidationShouldThrowValidationException()
        {
            //-- Setup method to return false so validation will fail
            cIdentityService.Setup(lService => lService.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(false));
            GenerateTokenCommandValidator lValidator = new GenerateTokenCommandValidator(cIdentityService.Object);

            ValidationBehavior<GenerateTokenCommand, AccessTokenReportModel> lValidationBehavior = new(new[] { lValidator });

            Assert.ThrowsAsync<ValidationException>(async () => await lValidationBehavior.Handle(cCommand, new CancellationToken(), null));
        }

        [Test]
        public async Task PassedValidationShouldCallNextDelegate()
        {
            Mock<RequestHandlerDelegate<AccessTokenReportModel>> nextDelegate = new Mock<RequestHandlerDelegate<AccessTokenReportModel>>();

            //-- Setup method to return false so validation will pass
            cIdentityService.Setup(lService => lService.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            GenerateTokenCommandValidator lValidator = new GenerateTokenCommandValidator(cIdentityService.Object);

            ValidationBehavior<GenerateTokenCommand, AccessTokenReportModel> lValidationBehavior = new(new[] { lValidator });

            await lValidationBehavior.Handle(cCommand, new CancellationToken(), nextDelegate.Object);

            nextDelegate.Verify(lDelegate => lDelegate(), Times.Once);
        }
    }
}