namespace workout_tracker_api.Contracts.User;

public sealed record UserLoginResponse
{
    public string Token { get; init; } = null!;
    public UserResponse User { get; init; } = null!;
}