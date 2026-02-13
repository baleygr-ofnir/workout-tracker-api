using AutoMapper;
using workout_tracker_api.Contracts.Workouts;
using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Mapping;

public class WorkoutProfile : Profile
{
    public WorkoutProfile()
    {
        CreateMap<Workout, WorkoutDto>();
        CreateMap<Exercise, ExerciseDto>();
        
        CreateMap<CreateWorkoutDto, Workout>();
        CreateMap<CreateExerciseDto, Exercise>();
    }
}