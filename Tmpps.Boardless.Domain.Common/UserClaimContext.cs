using System.Security.Claims;
using Tmpps.Boardless.Domain.Common.Models;
using Tmpps.Infrastructure.Common.Claims.Interfaces;
using Tmpps.Infrastructure.Common.Foundation.Interfaces;

namespace Tmpps.Boardless.Domain.Common
{
    public class UserClaimContext : IClaimContext<UserClaim>
    {
        private UserClaim userClaim;
        private IMapper mapper;
        private IJwtFactory jwtFactory;

        public UserClaimContext(
            IMapper mapper,
            IJwtFactory jwtFactory)
        {
            this.mapper = mapper;
            this.jwtFactory = jwtFactory;
        }

        public UserClaim Claim => this.userClaim;

        public void SetClaims(ClaimsPrincipal claimsPrincipal)
        {
            var jwtClaim = this.jwtFactory.Create(claimsPrincipal);
            this.userClaim = this.mapper.Map<UserClaim>(jwtClaim);
        }

        public void Impersonate(UserClaim claim)
        {
            this.userClaim = claim;
        }
    }
}