
using AutoMapper;
using System.Timers;
using WebApplication5LVL.AppData.Contexts.Mail;
using WebApplication5LVL.Contracts.User;

namespace WebApplication5LVL.AppData.Contexts.User
{
    /// <summary>
    /// UserService model, used to do CRUD operations by smart-repostitory with <see cref="WebApplication5LVL.Domain.Models.User"/>
    /// </summary>
    public sealed class UserService : IUserService
    {   
        private IMapper mapper { get; }
        private IUserRepository userRepository { get; }
        private IMailService mailService { get; }
        /// <summary>
        /// UserService model's constructor for state initialize
        /// </summary>
        /// <param name="_mapper"></param>
        /// <param name="_userRepository"></param>
        /// <param name="_mailService"></param>
        public UserService(IMapper _mapper, IUserRepository _userRepository, IMailService _mailService)
        {
            mapper = _mapper;
            userRepository = _userRepository;
            mailService = _mailService;
        }
        /// <summary>
        /// Async func, to add new record(line) in User table, by ORM technology
        /// </summary>
        /// <param name="createRequest"></param>
        /// <param name="photo"></param>
        /// <param name="token"></param>
        /// <returns><see cref="Task"/></returns>
        public async Task AddAsync(CreateUserRequest createRequest, byte[] photo, CancellationToken token = default)
        {
            Domain.Models.User model = mapper.Map<Domain.Models.User>(createRequest);

            model.birthDay = DateTime
                .Parse(createRequest.birthDay);

            model.photo = photo;

            System.Timers.Timer timer = new System.Timers.Timer(Math.Abs((DateTime.Now - model.birthDay).Milliseconds))
            {
                AutoReset = false
            };

            timer.Start();
            timer.Elapsed += async (object? sender, ElapsedEventArgs e)
                =>
            {
                string result = await mailService.SendMessage($"{model.SFL}, happy birthday to you! You are {DateTime.Now.Year - model.birthDay.Year} years old now!"
                , model.eMail
                , token);
            }; 

            await userRepository.AddAsync(model);
        }
        /// <summary>
        /// Async func, to delete  record(line) in User table, by ORM technology
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns><see cref="Task"/></returns>
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            Domain.Models.User user = await userRepository.FindByIdAsync(id);
            await userRepository.DeleteAsync(user);
        }
        /// <summary>
        /// Async func, to find record(line) from User table, by ORM technology, by <see cref="Guid"/> object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns><see cref="InfoUserResponse"/></returns>
        public async Task<InfoUserResponse> FindByIdAsync(Guid id, CancellationToken token = default)
        {
            Domain.Models.User user = await userRepository.FindByIdAsync(id);
            return mapper.Map<InfoUserResponse>(user);
        }
        /// <summary>
        /// Async func, to get all records(lines) from User table, by ORM technology
        /// </summary>
        /// <param name="token"></param>
        /// <returns><see cref="InfoUserResponse"/> collection</returns>
        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAllAsync(CancellationToken token = default)
        {
            IQueryable<Domain.Models.User> users = await userRepository.GetAllAsync();

            return users
                .Select(user => mapper.Map<InfoUserResponse>(user))
                .ToList();
        }
        /// <summary>
        /// Async func, to update existing record(line) from User table, by ORM technology
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateRequest"></param>
        /// <param name="token"></param>
        /// <returns><see cref="Task"></returns>
        public async Task UpdateAsync(Guid id, UpdateUserRequest updateRequest, CancellationToken token = default)
        {
            Domain.Models.User example = await userRepository.FindByIdAsync(id);
            Domain.Models.User user = mapper.Map(updateRequest, example);
            await userRepository.UpdateAsync(user);
        }
    }
}
