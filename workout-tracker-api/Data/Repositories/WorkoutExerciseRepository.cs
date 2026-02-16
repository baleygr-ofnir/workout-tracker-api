using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Data.Repositories;

public class WorkoutExerciseRepository : GenericRepository<WorkoutExercise>
{
    public WorkoutExerciseRepository(WorkoutContext context) : base(context)
    {
    }
}