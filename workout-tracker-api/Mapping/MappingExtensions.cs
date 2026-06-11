using workout_tracker_api.Contracts.Exercises;
using workout_tracker_api.Contracts.User;
using workout_tracker_api.Contracts.WorkoutExercises;
using workout_tracker_api.Contracts.Workouts;
using workout_tracker_api.Data.Entities;
using WorkoutResponse = workout_tracker_api.Contracts.WorkoutExercises.WorkoutResponse;

namespace workout_tracker_api.Mapping;

public static class MappingExtensions
{
    // Exercise mappings
    public static ExerciseResponse ToContract(this Exercise exercise)
    {
        return new ExerciseResponse
        {
            Id = exercise.Id,
            Name = exercise.Name,
            Description = exercise.Description,
            Category = exercise.Category
        };
    }

    public static Exercise ToEntity(this ExerciseCreateRequest contract)
    {
        return new Exercise
        {
            Name = contract.Name,
            Description = contract.Description,
            Category = contract.Category
        };
    }

    // Workout mappings
    public static Contracts.Workouts.WorkoutResponse ToContract(this Workout workout)
    {
        return new Contracts.Workouts.WorkoutResponse
        {
            Id = workout.Id,
            CreatedAt = workout.CreatedAt,
            Name = workout.Name,
            Description = workout.Description,
            WorkoutExercises = workout.WorkoutExercises.Select(we => we.ToContract()).ToList()
        };
    }

    public static Workout ToEntity(this WorkoutCreateRequest contract)
    {
        return new Workout
        {
            Name = contract.Name,
            Description = contract.Description
        };
    }

    // WorkoutExercise mappings
    public static WorkoutResponse ToContract(this WorkoutExercise workoutExercise)
    {
        return new WorkoutResponse
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

    public static WorkoutExercise ToEntity(this WorkoutExerciseCreateRequest contract)
    {
        return new WorkoutExercise
        {
            ExerciseId = contract.ExerciseId,
            Sets = contract.Sets,
            Reps = contract.Reps,
            Weight = contract.Weight
        };
    }

    public static UserResponse ToContract(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            IsActive = user.IsActive,
            IsAdmin = user.IsAdmin
        };
    }

    public static User ToEntity(this UserRegisterRequest newUser)
    {
        return new User
        {
            Username = newUser.Username,
            Email = newUser.Email,
            IsActive = true,
            IsAdmin = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null
        };
    }

    public static User ToEntity(this UserUpdateRequest updatedUser)
    {
        return new User
        {
            Username = updatedUser.Username ?? string.Empty,
            Email = updatedUser.Email ?? string.Empty,
            PasswordHash = string.Empty,
            IsActive = updatedUser.IsActive ?? false,
            IsAdmin = updatedUser.IsAdmin ?? false
        };
    }
}