using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication5LVL.AppData.Contexts.User;
using WebApplication5LVL.Contracts.User;

namespace WebApplication5LVL.API.Controllers
{
    [ApiController()]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;
        public UserController(IUserService _userService, ILogger<UserController> _logger)
        {
            userService = _userService;
            logger = _logger;
        }

        [HttpPost("/create")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateUserAsync(CreateUserRequest createRequest, CancellationToken token = default)
        {
            await userService.AddAsync(createRequest);

            logger.LogInformation($"User {createRequest.SFL} was added to database ");

            return Created("", "Add operation completed");
        }

        [HttpGet("/getById")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> FindUserByIdAsync(Guid id, CancellationToken token = default)
        {
            var user = await userService.FindByIdAsync(id);
            return Ok(user);
        }

        [HttpGet("/getAll")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUsersAsync(CancellationToken token = default)
        {
            IReadOnlyCollection<InfoUserResponse> users = await userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPut("/updateUser/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserInfoAsync(Guid id, UpdateUserRequest request, CancellationToken token = default)
        {
            await userService.UpdateAsync(id,request);
            return Ok();
        }

        [HttpDelete("/deleteUser/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteUserByIdAsync(Guid id, CancellationToken token = default)
        {
            await userService.DeleteAsync(id);
            return Ok();
        }
    }
}
