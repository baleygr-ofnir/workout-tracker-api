using Microsoft.EntityFrameworkCore;
using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Data;

public class WorkoutContext : DbContext
{
    public DbSet<Exercise> Exercises => Set<Exercise>(); 
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
    
    public WorkoutContext(DbContextOptions<WorkoutContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Workout>()
            .HasMany(w => w.WorkoutExercises)
            .WithOne(w => w.Workout)
            .HasForeignKey(e => e.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}