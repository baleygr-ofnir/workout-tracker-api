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
            .WithOne(we => we.Workout)
            .HasForeignKey(we => we.WorkoutId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WorkoutExercise>()
            .HasOne(we => we.Exercise)
            .WithMany()
            .HasForeignKey(we => we.ExerciseId);

        modelBuilder.Entity<Exercise>().HasData
        (
            new Exercise
            {
                Id = ExerciseSeed.PushUpId,
                Name = "Push Up",
                Description = "Bodyweight push exercise primarily targeting chest, shoulders, and triceps",
                Category = Category.PushPattern
            },
            new Exercise()
            {
                Id = ExerciseSeed.SquatId,
                Name = "Back Squat",
                Description = "Barbell squat targeting quads, glutes, and core.",
                Category = Category.SquatPattern
            },
            new Exercise()
            {
                Id = ExerciseSeed.DeadliftId,
                Name = "Deadlift",
                Description = "Hinge pattern lift targeting posterior chain and grip.",
                Category = Category.HingePattern
            }
        );
    }
}