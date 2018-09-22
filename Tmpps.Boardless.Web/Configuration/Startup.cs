using System;
using System.IO;
using System.Reflection;
using Tmpps.Infrastructure.AspNetCore.Extensions;
using Tmpps.Infrastructure.AspNetCore.Middlewares.Extensions;
using Tmpps.Infrastructure.Autofac.Builder;
using Tmpps.Infrastructure.Common.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tmpps.Boardless.Domain.Common.Models;
using Tmpps.Boardless.UseCases.Migration;

namespace Tmpps.Boardless.Web.Configuration
{
    public class Startup
    {
        private IHostingEnvironment hostingEnvironment;
        private ILoggerFactory loggerFactory;
        private IConfiguration configuration;
        private IConfigurationRoot configurationRoot;
        private Assembly executeAssembly;
        private string rootPath;
        private BoardlessWebConfig config;

        public Startup(IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.loggerFactory = loggerFactory;
            this.configuration = configuration;
            this.loggerFactory.AddConsole(this.configuration.GetSection("Logging"));
            this.loggerFactory.AddDebug();
            this.executeAssembly = Assembly.GetEntryAssembly();
            this.rootPath = Directory.GetCurrentDirectory();
            this.configurationRoot = this.CreateConfigurationRoot();
        }

        private IConfigurationRoot CreateConfigurationRoot()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional : true, reloadOnChange : true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional : true)
                .AddJsonFile($"appsettings.user.json", optional : true)
                .AddEnvironmentVariables();
            if (hostingEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets(this.executeAssembly);
            }
            builder.AddEnvironmentVariables();
            return builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            this.config = new BoardlessWebConfig(this.configurationRoot);
            services.AddWebApiService(this.config);
            services.AddMvc().AddControllersAsServices();

            var builder = new AutofacBuilder();
            builder.Populate(services);
            builder.RegisterModule(new BoardlessWebDIModule(this.executeAssembly, this.rootPath, this.configurationRoot, this.loggerFactory));
            var scope = builder.Build();
            var migrationService = scope.Resolve<IMigrationUseCase>();
            migrationService.ExecuteAsync().GetAwaiter().GetResult();
            return builder.CreateServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseWebApiServiceMiddlewares<UserClaim>(this.config);
        }
    }
}