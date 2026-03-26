using GameNLog.DTOs;
using GameNLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameNLog.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileDTO>> GetUser(int id)
        {

        }
    }
}
