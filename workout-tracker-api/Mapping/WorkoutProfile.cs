using AutoMapper;
using workout_tracker_api.Contracts.WorkoutExercises;
using workout_tracker_api.Contracts.Workouts;
using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Mapping;

public class WorkoutProfile : Profile
{
    public WorkoutProfile()
    {
        // Exercise mapping
        CreateMap<Exercise, ExerciseDto>();
        CreateMap<CreateExerciseDto, Exercise>();
       
        // Workout mapping
        CreateMap<Workout, WorkoutDto>();
        CreateMap<CreateWorkoutDto, Workout>();

        // WorkoutExerciseMapping
        CreateMap<WorkoutExercise, WorkoutExerciseDto>()
            .ForMember(dest => dest.WorkoutId, opt => opt.MapFrom(src => src.WorkoutId))
            .ForMember(dest => dest.ExerciseId, opt => opt.MapFrom(src => src.ExerciseId))
            .ForMember(dest => dest.ExerciseName, opt => opt.MapFrom(src => src.Exercise!.Name))
            .ForMember(dest => dest.ExerciseDescription, opt => opt.MapFrom(src => src.Exercise!.Description))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Exercise!.Category));
        CreateMap<CreateWorkoutExerciseDto, WorkoutExercise>();
    }
}