using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using workout_tracker_api.Contracts.WorkoutExercises;
using workout_tracker_api.Contracts.Workouts;
using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;
using workout_tracker_api.Mapping;
using WorkoutResponse = workout_tracker_api.Contracts.WorkoutExercises.WorkoutResponse;

namespace workout_tracker_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutsController : ControllerBase
{
    private readonly IRepository<Workout> _workouts;
    private readonly IRepository<WorkoutExercise> _workoutExercises;
    private readonly IRepository<Exercise> _exercises;
    
    public WorkoutsController(IRepository<Workout> workouts, IRepository<WorkoutExercise> workoutExercises, IRepository<Exercise> exercises) {
        _workouts = workouts;
        _workoutExercises = workoutExercises;
        _exercises = exercises;
    }
    
    // GET api/workouts/{workoutId}
    [HttpGet("{workoutId:guid}")]
    public async Task<ActionResult<Contracts.Workouts.WorkoutResponse>> GetWorkout(Guid workoutId, CancellationToken cancellationToken)
    {
        var workout = await _workouts.GetAsync(workoutId, cancellationToken); 
        if (workout == null) return NotFound();
        
        return Ok(workout.ToContract());
    }

    // POST api/workouts
    [HttpPost]
    public async Task<ActionResult<Contracts.Workouts.WorkoutResponse>> CreateWorkout([FromBody] WorkoutCreateRequest request,
        CancellationToken cancellationToken)
    {
        var workout = request.ToEntity();
        
        await _workouts.AddAsync(workout, cancellationToken);
        await _workouts.SaveChangesAsync(cancellationToken);

        return CreatedAtAction
            (
                nameof(GetWorkout),
                new { workoutId = workout.Id },
                workout.ToContract()
            );
    }

    // GET api/workouts/{workoutId}/exercises
    [HttpGet("{workoutId:guid}/exercises")]
    public async Task<ActionResult<IEnumerable<WorkoutResponse>>> GetWorkoutExercises(Guid workoutId,
        CancellationToken cancellationToken)
    {
        var workoutExercises = await _workoutExercises
            .FindAsync
            (
                we => we.WorkoutId == workoutId,
                cancellationToken
            );
        var workoutExerciseDtos = workoutExercises.Select(we => we.ToContract());
        
        return Ok(workoutExerciseDtos);
    }

    // POST api/workouts/{workoutId}/exercises
    [HttpPost("{workoutId:guid}/exercises")]
    public async Task<ActionResult<WorkoutResponse>> AddExercise(Guid workoutId, [FromBody] WorkoutExerciseCreateRequest request,
        CancellationToken cancellationToken)
    {
        var workout = await _workouts.GetAsync(workoutId, cancellationToken);
        if (workout == null) return NotFound();
        
        var exercise = await _exercises.GetAsync(request.ExerciseId, cancellationToken);
        if (exercise == null) return BadRequest("Exercise does not exist.");
        
        var workoutExercise = request.ToEntity();
        workoutExercise.WorkoutId = workoutId;
        workoutExercise.ExerciseId = request.ExerciseId;

        await _workoutExercises.AddAsync(workoutExercise, cancellationToken);
        await _workoutExercises.SaveChangesAsync(cancellationToken);
        
        workoutExercise.Exercise = exercise;
        
        return CreatedAtAction
            (
                nameof(GetWorkoutExercises),
                new { workoutId },
                workoutExercise.ToContract()
            );
    }
}