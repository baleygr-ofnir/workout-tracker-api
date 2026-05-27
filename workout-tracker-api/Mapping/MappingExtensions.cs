using workout_tracker_api.Contracts.Exercises;
using workout_tracker_api.Contracts.WorkoutExercises;
using workout_tracker_api.Contracts.Workouts;
using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Mapping;

public static class MappingExtensions
{
    // Exercise mappings
    public static ExerciseDto ToDto(this Exercise exercise)
    {
        return new ExerciseDto
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Description = exercise.Description,
            Category = exercise.Category
        };
    }

    public static Exercise ToEntity(this CreateExerciseDto dto)
    {
        return new Exercise
        {
            Name = dto.Name,
            Description = dto.Description,
            Category = dto.Category
        };
    }

    // Workout mappings
    public static WorkoutDto ToDto(this Workout workout)
    {
        return new WorkoutDto
        {
            Id = workout.Id,
            CreatedAt = workout.CreatedAt,
            Name = workout.Name,
            Description = workout.Description,
            WorkoutExercises = workout.WorkoutExercises.Select(we => we.ToDto()).ToList()
        };
    }

    public static Workout ToEntity(this CreateWorkoutDto dto)
    {
        return new Workout
        {
            Name = dto.Name,
            Description = dto.Description
        };
    }

    // WorkoutExercise mappings
    public static WorkoutExerciseDto ToDto(this WorkoutExercise workoutExercise)
    {
        return new WorkoutExerciseDto
        {
            Id = workoutExercise.Id,
            WorkoutId = workoutExercise.WorkoutId,
            ExerciseId = workoutExercise.ExerciseId,
            ExerciseName = workoutExercise.Exercise?.Name ?? string.Empty,
            ExerciseDescription = workoutExercise.Exercise?.Description ?? string.Empty,
            Category = workoutExercise.Exercise?.Category ?? default,
            Sets = workoutExercise.Sets,
            Reps = workoutExercise.Reps,
            Weight = workoutExercise.Weight
        };
    }

    public static WorkoutExercise ToEntity(this CreateWorkoutExerciseDto dto)
    {
        return new WorkoutExercise
        {
            ExerciseId = dto.ExerciseId,
            Sets = dto.Sets,
            Reps = dto.Reps,
            Weight = dto.Weight
        };
    }
}
