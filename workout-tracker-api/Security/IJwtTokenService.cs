using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Security;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}