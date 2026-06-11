using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;

namespace workout_tracker_api.Core.Services;

public class ExerciseService : GenericService<Exercise>
{
    public ExerciseService(IRepository<Exercise> repository) : base(repository)
    {
    }
}