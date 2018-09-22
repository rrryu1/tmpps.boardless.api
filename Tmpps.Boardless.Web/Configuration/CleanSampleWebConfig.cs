using System.Collections.Generic;
using System.Linq;
using Tmpps.Infrastructure.AspNetCore.Configuration.Interfaces;
using Tmpps.Infrastructure.Common.Configuration.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Tmpps.Boardless.Domain.Common.Configuration;

namespace Tmpps.Boardless.Web.Configuration
{
    public class BoardlessWebConfig : BoardlessConfig, IWebConfig
    {
        public BoardlessWebConfig(IConfigurationRoot configurationRoot) : base(configurationRoot)
        {
            this.IsEnableCors = this.configurationRoot.GetValue<bool>(nameof(IsEnableCors));
            this.UseAuthentication = this.configurationRoot.GetValue<bool>(nameof(UseAuthentication));
            this.IsUseSecure = this.configurationRoot.GetValue<bool>(nameof(IsUseSecure));
            this.CorsOrigins = this.configurationRoot.GetValue<string>(nameof(CorsOrigins));
        }

        public bool IsEnableCors { get; set; }
        public bool UseAuthentication { get; set; }
        public bool IsUseSecure { get; set; }
        public string CorsOrigins { get; set; }

        public IEnumerable<string> GetCorsOrigins()
        {
            return this.CorsOrigins?.Split(",") ?? Enumerable.Empty<string>();
        }

        public void CreateMvcConfigureRoutes(IRouteBuilder routes)
        {
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}