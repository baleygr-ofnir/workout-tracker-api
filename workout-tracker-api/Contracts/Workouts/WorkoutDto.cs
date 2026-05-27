using workout_tracker_api.Contracts.WorkoutExercises;

namespace workout_tracker_api.Contracts.Workouts;

public sealed record class WorkoutDto
{
    public required Guid Id { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required IReadOnlyList<WorkoutExerciseDto> WorkoutExercises { get; init; }
};
