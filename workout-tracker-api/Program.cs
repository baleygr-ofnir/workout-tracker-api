using Microsoft.EntityFrameworkCore;
using workout_tracker_api.Data;
using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;
using workout_tracker_api.Mapping;

namespace workout_tracker_api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        
        // DbContext
        builder.Services.AddDbContext<WorkoutContext>
        (
            options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        // DI for repositories
        builder.Services.AddScoped<IRepository<Exercise>, ExerciseRepository>();
        builder.Services.AddScoped<IRepository<Workout>, WorkoutRepository>();
        builder.Services.AddScoped<IRepository<WorkoutExercise>, WorkoutExerciseRepository>();
        // Automapping DTOs
        builder.Services.AddAutoMapper
        (
            _ =>
            {
            },
            typeof(WorkoutProfile).Assembly
        );
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}