namespace workout_tracker_api.Data.Entities;

public class WorkoutExercise
{
    public Guid Id { get; set; }
    public Guid WorkoutId { get; set; }
    public Guid ExerciseId { get; set; }
    public int Sets { get; set; }
    public int Reps { get; set; }
    public double? Weight { get; set; }
    public Workout? Workout { get; set; }
    public Exercise? Exercise { get; set; }
}