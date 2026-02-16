namespace workout_tracker_api.Contracts.WorkoutExercises;

public sealed class CreateWorkoutExerciseDto
{
    public Guid ExerciseId { get; init; }
    public int Sets { get; init; }
    public int Reps { get; init; }
    public double? Weight { get; init; }
}