using System.Threading;
using Tmpps.Infrastructure.Common.Claims.Interfaces;
using Tmpps.Infrastructure.Common.DependencyInjection.Builder;
using Tmpps.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Tmpps.Boardless.Domain.Common.Models;

namespace Tmpps.Boardless.Domain.Common
{
    public class BoardlessDomainCommonDIModule : IDIModule
    {
        private LifetimeType claimContextLifetimeType;

        public BoardlessDomainCommonDIModule(LifetimeType claimContextLifetimeType)
        {
            this.claimContextLifetimeType = claimContextLifetimeType;
        }
        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<UserClaimContext>(x => x.As<IClaimContext<UserClaim>>().LifetimeType = this.claimContextLifetimeType);
            builder.RegisterType<CancellationTokenSource>(x => x.SingleInstance());
        }
    }
}