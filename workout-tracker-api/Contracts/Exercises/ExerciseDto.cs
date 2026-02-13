namespace workout_tracker_api.Contracts.Workouts;

public sealed class ExerciseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Sets { get; init; }
    public int Reps { get; init; }
    public double? Weight { get; init; }
}