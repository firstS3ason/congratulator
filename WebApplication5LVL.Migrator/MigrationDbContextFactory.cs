using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WebApplication5LVL.Migrator
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<MigrationDbContext>
    {
        public MigrationDbContext CreateDbContext(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            string? connectionString = configuration.GetConnectionString("Default");

            DbContextOptionsBuilder<MigrationDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<MigrationDbContext>();
            dbContextOptionsBuilder.UseSqlServer(connectionString);
            return new MigrationDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
