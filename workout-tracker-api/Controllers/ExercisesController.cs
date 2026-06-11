using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using workout_tracker_api.Contracts.Exercises;
using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;
using workout_tracker_api.Mapping;

namespace workout_tracker_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExercisesController : ControllerBase
{
    private readonly IRepository<Exercise> _exercises;

    public ExercisesController(IRepository<Exercise> exercises)
    {
        _exercises = exercises;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseResponse>>> GetExercises(Guid exerciseId, CancellationToken cancellationToken)
    {
        var exercises = await _exercises.AllAsync(cancellationToken);
        var exerciseDtos = exercises.Select(e => e.ToContract());
        
        return Ok(exerciseDtos);
    }

    [HttpGet("{exerciseId:guid}")]
    public async Task<ActionResult<ExerciseResponse>> GetExercise(Guid exerciseId, CancellationToken cancellationToken)
    {
        var exercise = await _exercises.GetAsync(exerciseId, cancellationToken);
        if (exercise is null) return NotFound();
        
        return Ok(exercise.ToContract());
    }

    [HttpPost]
    public async Task<ActionResult<ExerciseResponse>> CreateExercise([FromBody] ExerciseCreateRequest request, CancellationToken cancellationToken)
    {
        var exercise = request.ToEntity();

        await _exercises.AddAsync(exercise, cancellationToken);
        await _exercises.SaveChangesAsync(cancellationToken);
        
        return CreatedAtAction
            (
                nameof(GetExercises),
                new { id = exercise.Id},
                exercise.ToContract()
            );
    }
}