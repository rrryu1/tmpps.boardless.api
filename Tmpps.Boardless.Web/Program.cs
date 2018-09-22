using System.IO;
using Tmpps.Boardless.Web.Configuration;
using Tmpps.Infrastructure.Autofac.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace Tmpps.Boardless.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(services => services.AddDI())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}