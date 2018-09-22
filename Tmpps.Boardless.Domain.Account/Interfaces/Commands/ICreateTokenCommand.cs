using System.Threading.Tasks;
using Tmpps.Boardless.Domain.Common.Models;

namespace Tmpps.Boardless.Domain.Account.Interfaces.Command
{
    public interface ICreateTokenCommand
    {
        Task<string> ExecuteAsync(UserClaim userClaim);
    }
}