using System.Threading.Tasks;
using Tmpps.Boardless.Domain.Account.Interfaces.Command;
using Tmpps.Boardless.Domain.Common.Models;
using Tmpps.Infrastructure.Common.Claims.Interfaces;

namespace Tmpps.Boardless.Domain.Account.Commands
{
    internal class CreateTokenCommand : ICreateTokenCommand
    {
        private IJwtFactory jwtFactory;

        public CreateTokenCommand(IJwtFactory jwtFactory)
        {
            this.jwtFactory = jwtFactory;
        }

        public async Task<string> ExecuteAsync(UserClaim userClaim)
        {
            return await Task.FromResult(this.jwtFactory.Create(userClaim));
        }
    }
}