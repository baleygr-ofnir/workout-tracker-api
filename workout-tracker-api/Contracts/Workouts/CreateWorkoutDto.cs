namespace workout_tracker_api.Contracts.Workouts;

public sealed record class CreateWorkoutDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
};