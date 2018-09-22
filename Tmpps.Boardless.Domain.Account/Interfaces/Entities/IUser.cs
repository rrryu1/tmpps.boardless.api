using System.Globalization;
using Tmpps.Boardless.Domain.Common.Constants;

namespace Tmpps.Boardless.Domain.Account.Interfaces.Entities
{
    public interface IUser
    {
        string UserId { get; set; }
        string Name { get; set; }
        string Email { get; set; }
        string EncreptedPassword { get; set; }
        CultureInfo CultureInfo { get; set; }
        UserState State { get; set; }
    }
}