namespace workout_tracker_api.Contracts.WorkoutExercises;

public sealed record WorkoutExerciseCreateRequest
{
    public required Guid ExerciseId { get; init; }
    public required int Sets { get; init; }
    public required int Reps { get; init; }
    public double? Weight { get; init; }
};