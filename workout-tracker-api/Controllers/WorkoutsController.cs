using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using workout_tracker_api.Contracts.WorkoutExercises;
using workout_tracker_api.Contracts.Workouts;
using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;

namespace workout_tracker_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutsController : ControllerBase
{
    private readonly IRepository<Workout> _workouts;
    private readonly IRepository<WorkoutExercise> _workoutExercises;
    private readonly IRepository<Exercise> _exercises;
    private readonly IMapper _mapper;
    
    public WorkoutsController(IRepository<Workout> workouts, IRepository<WorkoutExercise> workoutExercises, IRepository<Exercise> exercises, IMapper mapper) {
        _workouts = workouts;
        _workoutExercises = workoutExercises;
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
    public async Task<ActionResult<IEnumerable<WorkoutExerciseDto>>> GetWorkoutExercises(Guid workoutId,
        CancellationToken cancellationToken)
    {
        var workoutExercises = await _workoutExercises
            .FindAsync
            (
                we => we.WorkoutId == workoutId,
                cancellationToken
            );
        var workoutExerciseDtos = _mapper.Map<IEnumerable<WorkoutExerciseDto>>(workoutExercises);
        
        return Ok(workoutExerciseDtos);
    }

    // POST api/workouts/{workoutId}/exercises
    [HttpPost("{workoutId:guid}/exercises")]
    public async Task<ActionResult<WorkoutExerciseDto>> AddExercise(Guid workoutId, [FromBody] CreateWorkoutExerciseDto workoutExerciseDto,
        CancellationToken cancellationToken)
    {
        var workout = await _workouts.GetAsync(workoutId, cancellationToken);
        if (workout == null) return NotFound();
        
        var exercise = await _exercises.GetAsync(workoutExerciseDto.ExerciseId, cancellationToken);
        if (exercise == null) return BadRequest("Exercise does not exist.");
        
        var workoutExercise = _mapper.Map<WorkoutExercise>(workoutExerciseDto);
        workoutExercise.WorkoutId = workoutId;
        workoutExercise.ExerciseId = workoutExerciseDto.ExerciseId;

        await _workoutExercises.AddAsync(workoutExercise, cancellationToken);
        await _workoutExercises.SaveChangesAsync(cancellationToken);
        
        workoutExercise.Exercise = exercise;
        
        var result = _mapper.Map<WorkoutExerciseDto>(workoutExercise);

        return CreatedAtAction
            (
                nameof(GetWorkoutExercises),
                new { workoutId },
                result
            );
    }
}