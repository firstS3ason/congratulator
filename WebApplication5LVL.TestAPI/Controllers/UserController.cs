using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication5LVL.AppData.Contexts.User;
using WebApplication5LVL.Contracts.User;

namespace WebApplication5LVL.API.Controllers
{
    /// <summary>
    /// Controller for <see cref="IUserService"/> model
    /// </summary>
    [ApiController()]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// <see cref="IUserService"/> object for communication with <see cref="IUserRepository"/> object
        /// </summary>
        private readonly IUserService userService;
        /// <summary>
        /// Logger for recording actions
        /// </summary>
        private readonly ILogger<UserController> logger;
        /// <summary>
        /// <see cref="UserController"/> constructor
        /// </summary>
        /// <param name="_userService">UserService service, getting from IOC container</param>
        /// <param name="_logger">ILogger service, getting from IOC container</param>
        public UserController(IUserService _userService, ILogger<UserController> _logger)
        {
            userService = _userService;
            logger = _logger;
        }
        /// <summary>
        /// Request to create new user asynchronically in database
        /// </summary>
        /// <param name="createRequest">CreateUserRequest's object, DTO</param>
        /// <param name="token">Token for operation cancellation</param>
        /// <returns></returns>
        [HttpPost("/create")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest createRequest, IFormFile file, CancellationToken token = default)
        {
            byte[] photo = new byte[0];

            await using (MemoryStream ms = new MemoryStream())
            await using (Stream fs = file.OpenReadStream())
            {
               await fs.CopyToAsync(ms);
               photo = ms.ToArray();
            }

            await userService.AddAsync(createRequest, photo);
            
            logger.LogInformation($"User {createRequest.SFL} was added to database ");

            return Created("", "Add operation completed");
        }
        /// <summary>
        /// Request to find USER by identifier asynchronically from database
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <param name="token">Token for operation cancellation</param>
        /// <returns></returns>
        [HttpGet("/getById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> FindUserByIdAsync(Guid id, CancellationToken token = default)
        {
            var user = await userService.FindByIdAsync(id);
            return Ok(user);
        }
        /// <summary>
        /// Request to get all users asynchronically from database
        /// </summary>
        /// <param name="token">Token for operation cancellation</param>
        /// <returns></returns>
        [HttpGet("/getAll")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken token = default)
        {
            IReadOnlyCollection<InfoUserResponse> users = await userService.GetAllAsync();
            return Ok(users);
        }
        /// <summary>
        /// Request to update existing user's statement part asynchronically in database
        /// </summary>
        /// <param name="id">User's to update identifier</param>
        /// <param name="request">DTO of UpdateUserRequest</param>
        /// <param name="token">Token for operation cancellation</param>
        /// <returns></returns>
        [HttpPut("/updateUser/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserInfoAsync(Guid id, UpdateUserRequest request, CancellationToken token = default)
        {
            await userService.UpdateAsync(id,request);
            return Ok();
        }
        /// <summary>
        /// Request to delete existing user from database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete("/deleteUser/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUserByIdAsync(Guid id, CancellationToken token = default)
        {
            await userService.DeleteAsync(id);
            return Ok();
        }

        #region К УДАЛЕНИЮ
        [HttpGet("/gettto")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(CancellationToken token = default)
        {
            var info = await userService.FindByIdAsync(Guid.Parse("a4ae3b11-3fa6-49ba-96aa-08db6cf089a3"));
            return Ok(info);
        } 
        #endregion
    }
}
