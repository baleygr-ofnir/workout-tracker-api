using workout_tracker_api.Contracts.WorkoutExercises;

namespace workout_tracker_api.Contracts.Workouts;

public sealed class WorkoutDto
{
    public Guid Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IReadOnlyList<WorkoutExerciseDto> WorkoutExercises { get; init; } = Array.Empty<WorkoutExerciseDto>();
}