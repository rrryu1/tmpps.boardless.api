using Tmpps.Infrastructure.BoardlessData.Entities;
using Tmpps.Infrastructure.Common.Data.Interfaces;
using Tmpps.Infrastructure.Npgsql.Entity;
using Microsoft.EntityFrameworkCore;

namespace Tmpps.Infrastructure.BoardlessData
{
    public class BoardlessDataContext : NpgsqlDbContext
    {
        public BoardlessDataContext(DbContextOptions options) : base(options) { }
        public BoardlessDataContext(DbContextOptions<BoardlessDataContext> options) : base(options) { }
        public BoardlessDataContext(DbContextOptions options, IDbQueryCache queryPool) : base(options, queryPool) { }
        public BoardlessDataContext(DbContextOptions<BoardlessDataContext> options, IDbQueryCache queryPool) : base(options, queryPool) { }

        public DbSet<User> Users { get; set; }
    }
}