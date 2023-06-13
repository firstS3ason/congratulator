
using WebApplication5LVL.Contracts.User;

namespace WebApplication5LVL.AppData.Contexts.User
{
    internal interface IUserService
    {
        public Task<InfoUserResponse> FindById(Guid id, CancellationToken token = default);
        public Task AddAsync(CreateUserRequest createRequest, CancellationToken token = default);
        public Task DeleteAsync(Guid id, CancellationToken token = default);
        public Task UpdateAsync(UpdateUserRequest updateRequest, CancellationToken token = default);
    }
}
