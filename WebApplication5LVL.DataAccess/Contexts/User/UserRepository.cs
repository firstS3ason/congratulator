using WebApplication5LVL.AppData.Contexts.User;
using WebApplication5LVL.Infrastructure.Repositories;

namespace WebApplication5LVL.DataAccess.Contexts.User
{
    public class UserRepository : IUserRepository
    {
        public readonly IRepository<Domain.Models.User> repository;
        public UserRepository(IRepository<Domain.Models.User> _repository)
        => repository = _repository;
     
        public async Task AddAsync(Domain.Models.User user, CancellationToken token = default)
        => await repository.AddAsync(user, token);

        public async Task DeleteAsync(Domain.Models.User user, CancellationToken token = default)
        => await repository.DeleteAsync(user, token);

        public async Task<Domain.Models.User> FindByIdAsync(Guid id, CancellationToken token = default)
        => await repository.GetById(id, token);

        public IQueryable<Domain.Models.User> GetAll()
        => repository.GetAll();

        public async Task UpdateAsync(Domain.Models.User user, CancellationToken token = default)
        => await repository.UpdateAsync(user, token);
    }
}