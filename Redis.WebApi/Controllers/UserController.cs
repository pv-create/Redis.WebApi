using Microsoft.AspNetCore.Mvc;
using Redis.Services.DateBaseModels;
using Redis.Services.Interfaces;

namespace Redis.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            return await _userService.GetUser(id);
        }
    }
}
