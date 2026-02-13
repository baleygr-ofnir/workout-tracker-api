using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using workout_tracker_api.Contracts.Workouts;
using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;

namespace workout_tracker_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutsController : ControllerBase
{
    private readonly IRepository<Workout> _workouts;
    private readonly IRepository<Exercise> _exercises;
    private readonly IMapper _mapper;
    
    public WorkoutsController(IRepository<Workout> workouts, IRepository<Exercise> exercises, IMapper mapper) {
        _workouts = workouts;
        _exercises = exercises;
        _mapper = mapper;
    }
    
    // GET api/workouts/{workoutId}
    [HttpGet("{workoutId:guid}")]
    public async Task<ActionResult<WorkoutDto>> GetWorkout(Guid workoutId, CancellationToken cancellationToken)
    {
        var workout = await _workouts.GetAsync(workoutId, cancellationToken);
        if (workout == null) return NotFound();
        
        var workoutDto = _mapper.Map<WorkoutDto>(workout);
        return Ok(workoutDto);
    }

    // POST api/workouts
    [HttpPost]
    public async Task<ActionResult<WorkoutDto>> CreateWorkout([FromBody] CreateWorkoutDto workoutDto,
        CancellationToken cancellationToken)
    {
        var workout = _mapper.Map<Workout>(workoutDto);
        
        await _workouts.AddAsync(workout, cancellationToken);
        await _workouts.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<WorkoutDto>(workout);
        
        return CreatedAtAction
            (
                nameof(GetWorkout),
                new { workoutId = workout.Id },
                result
            );
    }

    // GET api/workouts/{workoutId}/exercises
    [HttpGet("{workoutId:guid}/exercises")]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetExercises(Guid workoutId,
        CancellationToken cancellationToken)
    {
        var exercises = await _exercises.FindAsync
            (
                e => e.WorkoutId == workoutId,
                cancellationToken
            );
        var exerciseDto = _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
        
        return Ok(exerciseDto);
    }

    // POST api/workouts/{workoutId}/exercises
    [HttpPost("{workoutId:guid}/exercises")]
    public async Task<ActionResult<ExerciseDto>> AddExercise(Guid workoutId, [FromBody] CreateExerciseDto exerciseDto,
        CancellationToken cancellationToken)
    {
        var exercise = _mapper.Map<Exercise>(exerciseDto);
        exercise.WorkoutId = workoutId;

        await _exercises.AddAsync(exercise, cancellationToken);
        await _exercises.SaveChangesAsync(cancellationToken);
        
        var result = _mapper.Map<ExerciseDto>(exercise);

        return CreatedAtAction
            (
                nameof(GetExercises),
                new { workoutId },
                result
            );
    }
}