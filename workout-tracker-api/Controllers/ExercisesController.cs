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
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetExercises(Guid exerciseId, CancellationToken cancellationToken)
    {
        var exercises = await _exercises.AllAsync(cancellationToken);
        var exerciseDtos = exercises.Select(e => e.ToDto());
        
        return Ok(exerciseDtos);
    }

    [HttpGet("{exerciseId:guid}")]
    public async Task<ActionResult<ExerciseDto>> GetExercise(Guid exerciseId, CancellationToken cancellationToken)
    {
        var exercise = await _exercises.GetAsync(exerciseId, cancellationToken);
        if (exercise is null) return NotFound();
        
        return Ok(exercise.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<ExerciseDto>> CreateExercise([FromBody] CreateExerciseDto exerciseDto, CancellationToken cancellationToken)
    {
        var exercise = exerciseDto.ToEntity();

        await _exercises.AddAsync(exercise, cancellationToken);
        await _exercises.SaveChangesAsync(cancellationToken);
        
        return CreatedAtAction
            (
                nameof(GetExercises),
                new { id = exercise.Id},
                exercise.ToDto()
            );
    }
}