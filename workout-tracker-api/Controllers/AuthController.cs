using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using workout_tracker_api.Contracts.User;
using workout_tracker_api.Core;
using workout_tracker_api.Core.Services;
using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        public AuthController(IService<User> userService)
        {
            _userService = userService as UserService ?? throw new Exception("UserService is unavailable");
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> RegisterUser([FromBody] UserRegisterRequest newUser)
        {
            var result = await _userService.RegisterUserAsync(newUser);
            if (result.Error is not null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> LoginUser([FromBody] UserLoginRequest request)
        {
            var result = await _userService.LoginUserAsync(request);
            if (result.Error is not null)
            {
                return Unauthorized(result.Error);
            }

            return Ok(result.Response);
        }
    }
}