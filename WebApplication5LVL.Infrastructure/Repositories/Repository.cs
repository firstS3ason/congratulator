using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WebApplication5LVL.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext dbContext { get; }
        protected DbSet<T> dbSet { get; }
        public Repository(DbContext _dbContext)
        {
           dbContext = _dbContext;
           dbSet = _dbContext.Set<T>();
        }
        
        public async Task AddAsync(T objectToAdd, CancellationToken token)
        {
            if (Equals(objectToAdd, null) && !(objectToAdd is T)) throw new ArgumentNullException(nameof(objectToAdd));
            await dbSet.AddAsync(objectToAdd);
            await dbContext.SaveChangesAsync();
        }
       
      
        public async Task DeleteAsync(T objectToDelete, CancellationToken token)
        {
            if (Equals(objectToDelete, null) && !(objectToDelete is T)) throw new ArgumentNullException(nameof(objectToDelete));
            await Task.Run(() => dbSet.Remove(objectToDelete));
            await dbContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll(CancellationToken token)
            => dbSet;

        public async Task<IQueryable<T>> GetAllFiltered(Expression<Func<T, bool>>? filter, CancellationToken token)
        {
            if(filter == null) throw new ArgumentNullException(nameof(filter));
            return await Task.Run(() => dbSet.Where(filter));
        }

        public async Task<T?> GetById(Guid id, CancellationToken token)
        => await Task.Run(async () => await dbSet.FindAsync(id,token));

        public async Task UpdateAsync(T objectToUpdate, CancellationToken token)
        {
            if (!(objectToUpdate is T) || Equals(objectToUpdate,null)) throw new ArgumentNullException(nameof(objectToUpdate));
            await Task.Run(() => dbSet.Update(objectToUpdate));
            await dbContext.SaveChangesAsync();
        }
    }
}
