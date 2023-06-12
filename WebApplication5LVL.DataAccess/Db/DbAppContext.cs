using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace WebApplication5LVL.DataAccess.Db
{
    public class DbAppContext : DbContext
    {
        public DbAppContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            t => t
            .GetInterfaces()
            .Any(i =>
            i.IsGenericType &&
            i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
    }
}
