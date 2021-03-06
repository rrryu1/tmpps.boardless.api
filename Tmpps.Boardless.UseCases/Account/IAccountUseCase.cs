using System.Threading.Tasks;

namespace Tmpps.Boardless.UseCases.Account
{
    public interface IAccountUseCase
    {
        Task<string> SignUpAsync(SignUpArgs args);
        Task<string> SignInAsync(SignInRequest args);
        Task<string> RefreshTokenAsync();
    }
}