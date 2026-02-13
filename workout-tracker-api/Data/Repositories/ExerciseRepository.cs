using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Data.Repositories;

public class ExerciseRepository : GenericRepository<Exercise>
{
    public ExerciseRepository(WorkoutContext context) : base(context)
    {
    }
}