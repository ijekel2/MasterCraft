using MasterCraft.Application.Authentication.Commands.GenerateToken;
using MasterCraft.Application.Common.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MasterCraft.Application.Integration.Testing;

namespace MasterCraft.Application.Integration.Authentication.Commands
{
    [TestFixture()]
    public class GenerateTokenTests : TestBase
    {
        [Test]
        public void ShouldFailIfNoUserNameOrPassword()
        {
            var command = new GenerateTokenCommand() 
            { 
                Username = string.Empty,
                Password = string.Empty
            };

            Assert.ThrowsAsync<McValidationException>(() => SendAsync(command));
        }
    }
}