using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Data.Repositories;

public class WorkoutRepository : GenericRepository<Workout>
{
    public WorkoutRepository(WorkoutContext context) : base(context)
    {
    }
}