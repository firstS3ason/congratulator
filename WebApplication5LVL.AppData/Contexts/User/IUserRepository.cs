
namespace WebApplication5LVL.AppData.Contexts.User
{
    public interface IUserRepository
    {
        public Task<Domain.Models.User> FindByIdAsync(Guid id, CancellationToken token = default);
        public IQueryable<Domain.Models.User> GetAll();
        public Task AddAsync(Domain.Models.User user, CancellationToken token = default);
        public Task DeleteAsync(Domain.Models.User user, CancellationToken token = default);
        public Task UpdateAsync(Domain.Models.User user, CancellationToken token = default);
    }
}
