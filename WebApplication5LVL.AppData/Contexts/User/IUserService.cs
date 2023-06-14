
using WebApplication5LVL.Contracts.User;

namespace WebApplication5LVL.AppData.Contexts.User
{
    public interface IUserService
    {
        public Task<InfoUserResponse> FindByIdAsync(Guid id, CancellationToken token = default);
        public Task AddAsync(CreateUserRequest createRequest, CancellationToken token = default);
        public Task DeleteAsync(Guid id, CancellationToken token = default);
        public Task UpdateAsync(Guid id, UpdateUserRequest updateRequest, CancellationToken token = default);
    }
}
