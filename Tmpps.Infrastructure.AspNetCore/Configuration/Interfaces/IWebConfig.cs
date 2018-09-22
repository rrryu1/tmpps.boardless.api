using System.Collections.Generic;
using Tmpps.Infrastructure.Common.Claims.Interfaces;
using Microsoft.AspNetCore.Routing;

namespace Tmpps.Infrastructure.AspNetCore.Configuration.Interfaces
{
    public interface IWebConfig : IJwtConfig
    {
        bool IsEnableCors { get; }
        bool UseAuthentication { get; }
        bool IsUseSecure { get; }

        IEnumerable<string> GetCorsOrigins();
        void CreateMvcConfigureRoutes(IRouteBuilder routes);
    }
}