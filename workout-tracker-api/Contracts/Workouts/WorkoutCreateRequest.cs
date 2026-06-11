namespace workout_tracker_api.Contracts.Workouts;

public sealed record WorkoutCreateRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
};