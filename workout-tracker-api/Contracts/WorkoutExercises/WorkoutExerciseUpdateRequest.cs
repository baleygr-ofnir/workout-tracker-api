namespace workout_tracker_api.Contracts.WorkoutExercises;

public sealed record WorkoutExerciseUpdateRequest()
{
    public int? Sets { get; init; }
    public int? Reps { get; init; }
    public double? Weight { get; init; }
}