using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Contracts.WorkoutExercises;

public sealed record WorkoutResponse
{
    public required Guid Id { get; init; }
    public required Guid ExerciseId { get; init; }
    public required Guid WorkoutId { get; init; }
    public required string ExerciseName { get; init; }
    public required string ExerciseDescription { get; init; }
    public required Category Category { get; init; }
    public required int Sets { get; init; }
    public required int Reps { get; init; }
    public double? Weight { get; init; }
};