using System.Linq.Expressions;

namespace WebApplication5LVL.Infrastructure.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<IQueryable<T>> GetAllAsync(CancellationToken token = default);
        public Task<IQueryable<T>> GetAllFiltered(Expression<Func<T, bool>> filter, CancellationToken token = default);
        public Task<T> GetById(Guid id, CancellationToken token = default);
        public Task AddAsync(T objectToAdd, CancellationToken token = default);
        public Task UpdateAsync(T objectToUpdate, CancellationToken token = default);
        public Task DeleteAsync(T objectToDelete, CancellationToken token = default);
    }
}
