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
        [Test]
        public void FailedValidationShouldThrowValidationException()
        {
            GenerateTokenCommand lCommand = new GenerateTokenCommand();
            Mock<IIdentityService> identityService = new Mock<IIdentityService>();

            //-- Setup method to return false so validation will fail
            identityService.Setup(lService => lService.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(false));
            GenerateTokenCommandValidator lValidator = new GenerateTokenCommandValidator(identityService.Object);

            ValidationBehavior<GenerateTokenCommand, AccessTokenReportModel> lValidationBehavior = new(new[] { lValidator });

            Assert.ThrowsAsync<ValidationException>(async () => await lValidationBehavior.Handle(lCommand, new CancellationToken(), null));
        }

        [Test]
        public async Task PassedValidationShouldCallNextDelegate()
        {
            GenerateTokenCommand lCommand = new GenerateTokenCommand();
            Mock<IIdentityService> identityService = new Mock<IIdentityService>();
            Mock<IValidator<IRequest>> validator = new Mock<IValidator<IRequest>>();

            Mock<RequestHandlerDelegate<AccessTokenReportModel>> nextDelegate = new Mock<RequestHandlerDelegate<AccessTokenReportModel>>();

            //-- Setup method to return false so validation will pass
            identityService.Setup(lService => lService.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            GenerateTokenCommandValidator lValidator = new GenerateTokenCommandValidator(identityService.Object);

            ValidationBehavior<GenerateTokenCommand, AccessTokenReportModel> lValidationBehavior = new(new[] { lValidator });

            await lValidationBehavior.Handle(lCommand, new CancellationToken(), nextDelegate.Object);

            nextDelegate.Verify(lDelegate => lDelegate(), Times.Once);
        }
    }
}