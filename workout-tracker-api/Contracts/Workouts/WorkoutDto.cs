namespace workout_tracker_api.Contracts.Workouts;

public sealed class WorkoutDto
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IReadOnlyList<ExerciseDto> Exercises { get; init; } = Array.Empty<ExerciseDto>();
}