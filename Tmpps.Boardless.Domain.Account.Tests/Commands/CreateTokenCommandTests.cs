using System;
using System.Threading.Tasks;
using Tmpps.Boardless.Domain.Account.Commands;
using Tmpps.Boardless.Domain.Common.Models;
using Tmpps.Infrastructure.Common.Claims.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Tmpps.Boardless.Domain.Account.Tests.Commands
{
    [TestClass]
    public class CreateTokenCommandTests
    {
        [TestMethod]
        public async Task ExecuteAsync()
        {
            var userClaim = new UserClaim();
            var expected = Guid.NewGuid().ToString();
            var mock = new Mock<IJwtFactory>();
            mock.Setup(x => x.Create(userClaim)).Returns(expected);
            var command = new CreateTokenCommand(mock.Object);
            var actual = await command.ExecuteAsync(userClaim);
            actual.Is(expected);
        }
    }
}