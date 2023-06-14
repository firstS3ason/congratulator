using Microsoft.AspNetCore.Mvc;
using WebApplication5LVL.AppData.Contexts.User;
using WebApplication5LVL.Contracts.User;

namespace WebApplication5LVL.API.Controllers
{
    [ApiController()]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
            => userService = _userService;
        [HttpPost("/create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateUser(CreateUserRequest createRequest, CancellationToken token = default)
        {
            await userService.AddAsync(createRequest);
            return Created("", "Add operation completed");
        }
        [HttpGet("/getId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FindUserById(Guid id, CancellationToken token = default)
        {
            var user = userService.FindByIdAsync(id);
            return Ok(user);
        }
    }
}
