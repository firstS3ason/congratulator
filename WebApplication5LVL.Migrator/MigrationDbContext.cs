using Microsoft.EntityFrameworkCore;
using WebApplication5LVL.DataAccess.Db;

namespace WebApplication5LVL.Migrator
{
    public class MigrationDbContext : DbAppContext
    {
        public MigrationDbContext(DbContextOptions options) : base(options) { }
    }
}
