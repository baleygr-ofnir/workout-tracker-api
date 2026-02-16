using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Contracts.WorkoutExercises;

public sealed class WorkoutExerciseDto
{
    public Guid Id { get; init; }
    public Guid ExerciseId { get; init; }
    public Guid WorkoutId { get; init; }
    public string ExerciseName { get; init; } = string.Empty;
    public string ExerciseDescription { get; init; } = string.Empty;
    public Category Category { get; init; }
    public int Sets { get; init; }
    public int Reps { get; init; }
    public double? Weight { get; init; }
}