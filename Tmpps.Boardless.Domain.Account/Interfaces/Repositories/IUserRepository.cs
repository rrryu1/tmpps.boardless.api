using System.Threading.Tasks;
using Tmpps.Boardless.Domain.Account.Interfaces.Entities;
using Tmpps.Boardless.Domain.Account.Models;
using Tmpps.Infrastructure.Common.Data.Interfaces;

namespace Tmpps.Boardless.Domain.Account.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<IUser>
    {
        Task<IUser> CreateAsync(UserCreationInfo user);
    }
}