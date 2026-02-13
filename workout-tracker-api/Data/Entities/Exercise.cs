namespace workout_tracker_api.Data.Entities;

public class Exercise
{
    public Guid Id { get; set; }
    public Guid WorkoutId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Sets { get; set; }
    public int Reps { get; set; }
    public double? Weight { get; set; }
    public Workout? Workout { get; set; }
}