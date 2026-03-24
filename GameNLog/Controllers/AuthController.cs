using GameNLog.DTOs;
using GameNLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameNLog.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);
            if (!result.Success)
                return Conflict(new { message = result.Error });
            return Ok(new { token = result.Token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);
            if (!result.Success)
                return Unauthorized(new { message = result.Error });
            return Ok(new { token = result.Token });

        }

    }
}
