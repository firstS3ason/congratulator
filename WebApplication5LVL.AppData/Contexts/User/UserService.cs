
using AutoMapper;
using System.Timers;
using WebApplication5LVL.AppData.Contexts.Mail;
using WebApplication5LVL.Contracts.User;
using WebApplication5LVL.AppData.Contexts.Telegram.ExtensionsMethods;

namespace WebApplication5LVL.AppData.Contexts.User
{
    public sealed class UserService : IUserService
    {
        private IMapper mapper { get; }
        private IUserRepository userRepository { get; }
        private IMailService mailService { get; }
        public UserService(IMapper _mapper, IUserRepository _userRepository, IMailService _mailService)
        {
            mapper = _mapper;
            userRepository = _userRepository;
            mailService = _mailService;
        }

        public async Task AddAsync(CreateUserRequest createRequest,byte[] photo, CancellationToken token = default)
        {
            Domain.Models.User model = mapper.Map<Domain.Models.User>(createRequest);
            model.photo = photo;


            System.Timers.Timer timer = new System.Timers.Timer(Math.Abs((DateTime.Now - model.birthDay).Milliseconds));
            timer.Start();

            timer.Elapsed += async (object? sender, ElapsedEventArgs e)
                =>
            {
                string result = await mailService.SendMessage($"{model.SFL}, happy birthday to you! With {new DateTime().GetNextBirthdayDate() - DateTime.Now} "
                , model.eMail
                , token);
                Thread.Sleep(100);
            };
            timer.AutoReset = false;
 

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
