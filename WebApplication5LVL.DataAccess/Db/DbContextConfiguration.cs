using Microsoft.EntityFrameworkCore;

namespace WebApplication5LVL.DataAccess.Db
{
    public class DbContextConfiguration : IDbContextOptionsConfigurator<DbAppContext>
    {
        public void Configure(DbContextOptionsBuilder<DbAppContext> optBuilder)
        {

        }
    }
}
