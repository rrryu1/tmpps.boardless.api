using System.Runtime.CompilerServices;
using Tmpps.Boardless.Domain.Account.Commands;
using Tmpps.Boardless.Domain.Account.Interfaces.Command;
using Tmpps.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
[assembly : InternalsVisibleTo("Tmpps.Boardless.Domain.Account.Tests")]
namespace Tmpps.Boardless.Domain.Account
{
    public class DomainAccountDIModule : IDIModule
    {
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<CreateTokenCommand>(x => x.As<ICreateTokenCommand>());
            builder.RegisterType<CreateUserCommand>(x => x.As<ICreateUserCommand>());
        }
    }
}