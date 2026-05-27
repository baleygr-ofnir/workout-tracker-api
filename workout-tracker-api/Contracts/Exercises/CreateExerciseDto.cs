using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Contracts.Exercises;

public sealed record class CreateExerciseDto
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required Category Category { get; init; }
};
