using Tmpps.Infrastructure.BoardlessData.Repositories;
using Tmpps.Infrastructure.Common.Data;
using Tmpps.Infrastructure.Common.Data.Entity.Interfaces;
using Tmpps.Infrastructure.Common.Data.Interfaces;
using Tmpps.Infrastructure.Common.DependencyInjection.Builder.Interfaces;
using Tmpps.Infrastructure.Npgsql.Entity;
using Tmpps.Infrastructure.Npgsql.Entity.Wrapper;
using Microsoft.EntityFrameworkCore;
using Tmpps.Boardless.Domain.Account.Interfaces.Repositories;
using Tmpps.Boardless.UseCases.Interfaces.Queries;

namespace Tmpps.Infrastructure.BoardlessData
{
    public class BoardlessDataDIModule : IDIModule
    {
        private string connectionString;

        public BoardlessDataDIModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void DefineModule(IDIBuilder builder)
        {
            builder.RegisterType<UserRepository>(x => x.As<IUserRepository>().As<IUserQuery>());
            var optionsBuilder = new DbContextOptionsBuilder<BoardlessDataContext>();
            optionsBuilder.UseNpgsql(this.connectionString);
            builder.RegisterInstance(optionsBuilder.Options);
            builder.RegisterType<BoardlessDataContext>(x => x.InstancePerLifetimeScope());
            builder.RegisterType<DbContextWrapper<BoardlessDataContext>>(x => x.As<IDbContext>().As<IDbTransactionManager>().InstancePerLifetimeScope());
            builder.RegisterType<DbQueryCache>(x => x.As<IDbQueryCache>().SingleInstance());
        }
    }
}