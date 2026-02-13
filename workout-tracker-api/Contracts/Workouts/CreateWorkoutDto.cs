namespace workout_tracker_api.Contracts.Workouts;

public sealed class CreateWorkoutDto
{
    public DateTime Date { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}