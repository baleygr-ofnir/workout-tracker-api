namespace workout_tracker_api.Contracts.User;

public sealed record UserUpdateRequest()
{
    public string? Username { get; init; }
    public string? Email { get; init; }
    public string? Password { get; init; }
    public bool? IsActive { get; set; }
    public bool? IsAdmin { get; set; }
}