using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Contracts.Workouts;

public sealed record WorkoutUpdateRequest()
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public ICollection<WorkoutExercise>? WorkoutExercises { get; init; }
}