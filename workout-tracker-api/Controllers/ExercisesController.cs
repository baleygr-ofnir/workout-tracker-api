using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using workout_tracker_api.Contracts.Workouts;
using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;

namespace workout_tracker_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExercisesController : ControllerBase
{
    private readonly IRepository<Exercise> _exercises;
    private readonly IMapper _mapper;

    public ExercisesController(IRepository<Exercise> exercises, IMapper mapper)
    {
        _exercises = exercises;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExerciseDto>>> GetExercises(Guid exerciseId, CancellationToken cancellationToken)
    {
        var exercises = await _exercises.AllAsync(cancellationToken);
        var exerciseDtos = _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
        
        return Ok(exerciseDtos);
    }

    [HttpGet("{exerciseId:guid}")]
    public async Task<ActionResult<ExerciseDto>> GetExercise(Guid exerciseId, CancellationToken cancellationToken)
    {
        var exercise = await _exercises.GetAsync(exerciseId, cancellationToken);
        if (exercise is null) return NotFound();
        
        var exerciseDto = _mapper.Map<ExerciseDto>(exercise);
        return Ok(exerciseDto);
    }

    [HttpPost]
    public async Task<ActionResult<ExerciseDto>> CreateExercise([FromBody] CreateExerciseDto exerciseDto, CancellationToken cancellationToken)
    {
        var exercise = _mapper.Map<Exercise>(exerciseDto);

        await _exercises.AddAsync(exercise, cancellationToken);
        await _exercises.SaveChangesAsync(cancellationToken);
        
        var result= _mapper.Map<ExerciseDto>(exercise);
        return CreatedAtAction
            (
                nameof(GetExercises),
                new { id = exercise.Id},
                result
            );
    }
}