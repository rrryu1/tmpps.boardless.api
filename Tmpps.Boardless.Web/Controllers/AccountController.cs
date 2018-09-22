using System.Threading.Tasks;
using Tmpps.Boardless.UseCases.Account;
using Tmpps.Infrastructure.Common.Foundation.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tmpps.Boardless.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAccountUseCase AccountUseCase;
        private IMapper mapper;

        public AccountController(
            IAccountUseCase AccountUseCase,
            IMapper mapper)
        {
            this.AccountUseCase = AccountUseCase;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> SignIn([FromBody] SignInRequest args)
        {
            return await this.AccountUseCase.SignInAsync(args);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<string> SignUp([FromBody] SignUpArgs args)
        {
            return await this.AccountUseCase.SignUpAsync(args);
        }

        [Authorize]
        [HttpPost]
        public async Task<string> RefreshToken()
        {
            return await this.AccountUseCase.RefreshTokenAsync();
        }
    }
}