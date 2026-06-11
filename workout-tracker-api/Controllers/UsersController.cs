using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using workout_tracker_api.Contracts.User;
using workout_tracker_api.Contracts.Workouts;
using workout_tracker_api.Core;
using workout_tracker_api.Core.Services;
using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;

namespace workout_tracker_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IService<Workout> _workoutService;

        public UsersController(IService<User> userService, IService<Workout> workoutService)
        {
            _userService = userService as UserService ?? throw new Exception("UserService is unavailable.");
            _workoutService = workoutService;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserResponse>> GetUser([FromRoute] Guid id)
        {
            var result = await _userService.GetAsync(id);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserResponse>> GetByUsername([FromRoute] string username)
        {
            var result = await _userService.GetByUsernameAsync(username);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsers()
        {
            var result = await _userService.GetUsers();

            return result.Any() ? Ok(result) : NotFound();
        }

        [HttpPut("{id:guid}")]
//        [Authorize]
        public async Task<ActionResult<UserResponse>> UpdateUser([FromRoute] Guid id,
            [FromBody] UserUpdateRequest request)
        {
            /*var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var currentUserId))
            {
                return Unauthorized();
            }

            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin && currentUserId != id)
            {
                return Forbid();
            }

            if (!isAdmin && request.IsAdmin == true)
            {
                return StatusCode(StatusCodes.Status403Forbidden,
                    "Only administrators may enable administrator status on users.");
            }*/

            var result = await _userService.UpdateUserAsync(id, request);

            return result.Error is null ? Ok(result) : BadRequest(result.Error);
        }

        [HttpDelete("{id:guid}")]
//        [Authorize]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(userIdValue) || !Guid.TryParse(userIdValue, out var currentUserId))
            {
                return Unauthorized();
            }

            var isAdmin = User.IsInRole("Admin");
            if (!isAdmin && currentUserId != id)
            {
                return Forbid();
            }

            bool result = await _userService.Delete(id);
            if (!result) return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "User deletion was unsuccessful."
                );

            return NoContent();
        }

        [HttpGet("{id:guid}/workouts/{workoutId:guid}")]
        [Authorize]
        public async Task<ActionResult<WorkoutResponse>> GetUserWorkout([FromRoute] Guid id, [FromRoute] Guid workoutId)
        {
            var resultList = await _workoutService.FindAsync(w => w.Id == workoutId && w.CreatorId == id);
            var result = resultList.FirstOrDefault();

            return result is not null
                ? Ok(result)
                : NotFound();
        }

        [HttpGet("{id:guid}/workouts")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<WorkoutResponse>>> GetUserWorkouts([FromRoute] Guid id)
        {
            var results = await _workoutService.FindAsync(w => w.CreatorId == id);
            return results.Any()
                ? Ok(results)
                : Ok(Array.Empty<WorkoutResponse>());
        }
    }
}