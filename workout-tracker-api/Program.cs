using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using workout_tracker_api.Core;
using workout_tracker_api.Core.Services;
using workout_tracker_api.Data;
using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;
using workout_tracker_api.Security;

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
            options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
        // DI for repositories
        builder.Services.AddScoped<IRepository<Exercise>, ExerciseRepository>();
        builder.Services.AddScoped<IRepository<Workout>, WorkoutRepository>();
        builder.Services.AddScoped<IRepository<WorkoutExercise>, WorkoutExerciseRepository>();
        builder.Services.AddScoped<IRepository<User>, UserRepository>();

        // DI for services
        builder.Services.AddScoped<IService<Exercise>, ExerciseService>();
        builder.Services.AddScoped<IService<Workout>, WorkoutService>();
        builder.Services.AddScoped<IService<User>, UserService>();

        // DI for security
        builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

        var jwtSigningKey= builder.Configuration.GetValue<string>("Jwt:SigningKey")!;
        var jwtIssuer = builder.Configuration.GetValue<string>("Jwt:Issuer")!;
        var jwtAudience = builder.Configuration.GetValue<string>("Jwt:Audience")!;

        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSigningKey))
                };
            });
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}