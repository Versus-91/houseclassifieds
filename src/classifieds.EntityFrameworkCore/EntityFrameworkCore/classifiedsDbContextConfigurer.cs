using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace classifieds.EntityFrameworkCore
{
    public static class classifiedsDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<classifiedsDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<classifiedsDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
