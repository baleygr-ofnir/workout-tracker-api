using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Contracts.Workouts;

public sealed class ExerciseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Category Category { get; init; }
}