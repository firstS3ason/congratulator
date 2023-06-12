using Microsoft.EntityFrameworkCore;

namespace WebApplication5LVL.DataAccess.Db
{
    public interface IDbContextOptionsConfigurator<TContext> where TContext: DbContext
    {
        public void Configure(DbContextOptionsBuilder<TContext> opt);
    }
}
