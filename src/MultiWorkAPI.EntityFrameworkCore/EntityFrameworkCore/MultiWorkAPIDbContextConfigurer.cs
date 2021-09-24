using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MultiWorkAPI.EntityFrameworkCore
{
    public static class MultiWorkAPIDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MultiWorkAPIDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MultiWorkAPIDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
