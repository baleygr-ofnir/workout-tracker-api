namespace workout_tracker_api.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Workout> Workouts { get; set; } = new List<Workout>();
}