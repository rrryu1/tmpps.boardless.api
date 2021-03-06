using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tmpps.Infrastructure.BoardlessData.Entities;
using Tmpps.Infrastructure.Common.Claims.Interfaces;
using Tmpps.Infrastructure.Common.Cryptography.Interfaces;
using Tmpps.Infrastructure.Common.Data.Entity.Interfaces;
using Tmpps.Infrastructure.Common.Foundation.Interfaces;
using Tmpps.Infrastructure.Npgsql.Entity.Repositories;
using Microsoft.EntityFrameworkCore;
using Tmpps.Boardless.Domain.Account.Interfaces.Entities;
using Tmpps.Boardless.Domain.Account.Interfaces.Repositories;
using Tmpps.Boardless.Domain.Account.Models;
using Tmpps.Boardless.Domain.Common.Constants;
using Tmpps.Boardless.Domain.Common.Models;
using Tmpps.Boardless.UseCases.Account;
using Tmpps.Boardless.UseCases.Interfaces.Queries;

namespace Tmpps.Infrastructure.BoardlessData.Repositories
{
    public class UserRepository : DbRepositoryBase<User>, IUserRepository, IUserQuery
    {
        private IGuidFactory guidFactory;
        private IMapper mapper;
        private IHashComputer hashComputer;
        private IClaimContext<UserClaim> claimContext;

        public UserRepository(
            Lazy<IDbContext> dbContextLazy,
            IGuidFactory guidFactory,
            IMapper mapper,
            IHashComputer hashComputer,
            IClaimContext<UserClaim> claimContext) : base(dbContextLazy)
        {
            this.guidFactory = guidFactory;
            this.mapper = mapper;
            this.hashComputer = hashComputer;
            this.claimContext = claimContext;
        }
        public async Task<IUser> CreateAsync(UserCreationInfo createUserArgs)
        {
            var user = this.mapper.Map<User>(createUserArgs);
            user.UserId = this.guidFactory.CreateNew().ToString();
            user.State = UserState.Active;
            user.EncreptedPassword = this.hashComputer.Compute(createUserArgs.Password);
            await this.AddAsync(user);
            return await Task.FromResult(user);
        }

        private async Task<IUser> GetAsync(string userId)
        {
            return await this.Query.SingleOrDefaultAsync(u =>
                u.UserId == userId &&
                u.State == UserState.Active);
        }

        private async Task<IUser> GetAsync(string email, string password)
        {
            var encreptedPassword = this.hashComputer.Compute(password);
            return await this.Query.SingleOrDefaultAsync(u =>
                u.Email == email &&
                u.EncreptedPassword == encreptedPassword &&
                u.State == UserState.Active);
        }

        public async Task<UserClaim> GetCurrentUserClaimAsync()
        {
            var user = await this.GetAsync(this.claimContext.Claim.UserId);
            return this.mapper.Map<UserClaim>(user);
        }

        public async Task<UserClaim> GetSignInUserClaimAsync(SignInRequest args)
        {
            var user = await this.GetAsync(args.Email, args.Password);
            return this.mapper.Map<UserClaim>(user);
        }

        protected override Expression<Func<User, bool>> GetKeyExpression(string id)
        {
            return e => e.UserId == id;
        }
    }
}