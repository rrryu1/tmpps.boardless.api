using Tmpps.Infrastructure.Common.Claims.Interfaces;
using Tmpps.Infrastructure.Common.Configuration.Interfaces;
using Tmpps.Infrastructure.Common.Data.Configuration.Interfaces;
using Tmpps.Infrastructure.Common.Data.Migration.Interfaces;
using Tmpps.Infrastructure.Common.Messaging.Interfaces;

namespace Tmpps.Boardless.Domain.Common.Interfaces
{
    public interface IBoardlessConfig : IConfig, IDbConfig, IJwtConfig, IMigrationConfig, ISQSConfig
    {

    }
}