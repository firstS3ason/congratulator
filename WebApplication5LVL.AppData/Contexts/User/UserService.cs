
using WebApplication5LVL.Contracts.User;

namespace WebApplication5LVL.AppData.Contexts.User
{
    internal class UserService : IUserService
    {
        public Task AddAsync(CreateUserRequest createRequest, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task<InfoUserResponse> FindById(Guid id, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateUserRequest updateRequest, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
