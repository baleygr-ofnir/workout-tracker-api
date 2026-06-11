using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Contracts.Exercises;

public sealed record ExerciseResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Category Category { get; init; }
};