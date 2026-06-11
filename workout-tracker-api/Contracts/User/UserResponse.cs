namespace workout_tracker_api.Contracts.User;

public sealed record UserResponse
{
    public Guid Id { get; init; }
    public string Username { get; init; } = null!;
    public string Email { get; init; } = null!;
    public bool IsActive { get; init; }
    public bool IsAdmin { get; init; }
}