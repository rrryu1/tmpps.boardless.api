using System.Threading.Tasks;
using Tmpps.Boardless.Domain.Account.Models;
using Tmpps.Boardless.Domain.Common.Models;

namespace Tmpps.Boardless.Domain.Account.Interfaces.Command
{
    public interface ICreateUserCommand
    {
        Task<UserClaim> ExecuteAsync(UserCreationInfo userCreationInfo);
        Task SendMailForNewUserAsync(UserClaim claim);
    }
}