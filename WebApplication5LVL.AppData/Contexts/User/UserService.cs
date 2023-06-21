
using AutoMapper;
using WebApplication5LVL.Contracts.User;

namespace WebApplication5LVL.AppData.Contexts.User
{
    public sealed class UserService : IUserService
    {
        private IMapper mapper { get; }
        private IUserRepository userRepository { get; }
        public UserService(IMapper _mapper, IUserRepository _userRepository)
        {
            mapper = _mapper;
            userRepository = _userRepository;
        }

        public async Task AddAsync(CreateUserRequest createRequest,byte[] photo, CancellationToken token = default)
        {
            Domain.Models.User model = mapper.Map<Domain.Models.User>(createRequest);
            model.photo = photo;
            await userRepository.AddAsync(model);
        }

        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            Domain.Models.User user = await userRepository.FindByIdAsync(id);
            await userRepository.DeleteAsync(user);
        }

        public async Task<InfoUserResponse> FindByIdAsync(Guid id, CancellationToken token = default)
        {
            Domain.Models.User user = await userRepository.FindByIdAsync(id);
            return mapper.Map<InfoUserResponse>(user);

        }
        
        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(CancellationToken token = default)
        {
            IQueryable<Domain.Models.User> users = await userRepository.GetAllAsync();

            return users
                .Select(user => mapper.Map<InfoUserResponse>(user))
                .ToList();
        }

        public async Task UpdateAsync(Guid id, UpdateUserRequest updateRequest, CancellationToken token = default)
        {
            Domain.Models.User example = await userRepository.FindByIdAsync(id);
            Domain.Models.User user = mapper.Map(updateRequest, example);
            await userRepository.UpdateAsync(user);
        }
    }
}
