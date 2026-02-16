using Microsoft.EntityFrameworkCore;
using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Data.Repositories;

public class WorkoutRepository : GenericRepository<Workout>
{
    public WorkoutRepository(WorkoutContext context) : base(context)
    {
    }

    public override async Task<Workout?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Context.Workouts
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise)
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
    }

    public override async Task<IReadOnlyList<Workout>> AllAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Workouts
            .AsNoTracking()
            .Include(w => w.WorkoutExercises)
            .ThenInclude(we => we.Exercise)
            .ToListAsync(cancellationToken);
    }
}