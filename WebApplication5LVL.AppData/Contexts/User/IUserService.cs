
using WebApplication5LVL.Contracts.User;

namespace WebApplication5LVL.AppData.Contexts.User
{
    /// <summary>
    /// Abstract contract model, implemented in <see cref="UserService"/>
    /// </summary>
    public interface IUserService
    {
        public Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(CancellationToken token = default);
        public Task<InfoUserResponse> FindByIdAsync(Guid id, CancellationToken token = default);
        public Task AddAsync(CreateUserRequest createRequest, byte[] photo,  CancellationToken token = default);
        public Task DeleteAsync(Guid id, CancellationToken token = default);
        public Task UpdateAsync(Guid id, UpdateUserRequest updateRequest, CancellationToken token = default);
    }
}
