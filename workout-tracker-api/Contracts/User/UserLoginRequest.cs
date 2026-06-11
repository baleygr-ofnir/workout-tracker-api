namespace workout_tracker_api.Contracts.User;

public sealed record UserLoginRequest()
{
    public string UsernameOrEmail { get; init; } = null!;
    public string Password { get; set; } = null!;
}