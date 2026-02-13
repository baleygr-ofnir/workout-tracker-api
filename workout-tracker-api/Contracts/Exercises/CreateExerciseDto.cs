namespace workout_tracker_api.Contracts.Workouts;

public sealed class CreateExerciseDto
{
    public string Name { get; init; } = string.Empty;
    public int Sets { get; init; }
    public int Reps { get; init; }
    public double? Weight { get; init; }
}