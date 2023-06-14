using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApplication5LVL.DataAccess.Db
{
    public class DbContextConfiguration : IDbContextOptionsConfigurator<DbAppContext>
    {
        private readonly IConfiguration configuration;
        private readonly ILoggerFactory loggerFactory;
        public DbContextConfiguration(IConfiguration _configuration, ILoggerFactory _loggerFactory)
        {
            configuration = _configuration;
            loggerFactory = _loggerFactory;
        }
        public void Configure(DbContextOptionsBuilder<DbAppContext> optBuilder)
        {
            var connectionString = configuration.GetConnectionString("Default");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"Не найдена строка подключения с именем 'Default'");
            }
            optBuilder.UseSqlServer(connectionString);
            optBuilder.UseLoggerFactory(loggerFactory);
        }
    }
}
