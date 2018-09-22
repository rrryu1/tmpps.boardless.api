using System.Threading.Tasks;
using Tmpps.Boardless.Domain.Common.Models;
using Tmpps.Boardless.UseCases.Account;

namespace Tmpps.Boardless.UseCases.Interfaces.Queries
{
    public interface IUserQuery
    {
        Task<UserClaim> GetCurrentUserClaimAsync();
        Task<UserClaim> GetSignInUserClaimAsync(SignInRequest args);
    }
}