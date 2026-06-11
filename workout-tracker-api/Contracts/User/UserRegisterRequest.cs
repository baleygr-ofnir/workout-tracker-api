namespace workout_tracker_api.Contracts.User;

public sealed record UserRegisterRequest()
{
    public string Username { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}