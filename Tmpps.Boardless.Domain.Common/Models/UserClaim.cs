using System.Globalization;
using Tmpps.Infrastructure.Common.Claims.Interfaces;

namespace Tmpps.Boardless.Domain.Common.Models
{
    public class UserClaim : IJwtClaimInfo
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}