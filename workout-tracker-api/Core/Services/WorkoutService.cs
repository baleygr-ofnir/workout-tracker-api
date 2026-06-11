using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;

namespace workout_tracker_api.Core.Services;

public class WorkoutService : GenericService<Workout>
{
    private readonly IRepository<WorkoutExercise> _workoutExerciseRepository;
    public WorkoutService(IRepository<Workout> workoutRepository, IRepository<WorkoutExercise> workoutExerciseRepository) : base(workoutRepository)
    {
        _workoutExerciseRepository = workoutExerciseRepository;
    }
}