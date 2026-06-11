using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using workout_tracker_api.Contracts.User;
using workout_tracker_api.Data.Entities;
using workout_tracker_api.Data.Repositories;
using workout_tracker_api.Mapping;
using workout_tracker_api.Security;

namespace workout_tracker_api.Core.Services;

public class UserService : GenericService<User>
{
    private readonly UserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;

    public UserService(IRepository<User> repository, IPasswordHasher<User> passwordHasher, IJwtTokenService jwtTokenService) : base(repository)
    {
        _userRepository = Repository as UserRepository ?? throw new Exception("UserRepository is unavailable");
        _passwordHasher = passwordHasher;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<(UserResponse? Response, string? Error)> RegisterUserAsync(UserRegisterRequest request)
    {
        var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUsername is not null)
        {
            return (null, "Username is already registered to another user");
        }

        var existingEmail = await _userRepository.GetByEmailAsync(request.Email);
        if (existingEmail is not null)
        {
            return (null, "Email is already registered to another user.");
        }

        var user = request.ToEntity();
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        var added = await AddAsync(user);

        var response = added.ToContract();

        return (response, null);
    }

    public async Task<(UserLoginResponse? Response, string? Error)> LoginUserAsync(UserLoginRequest request)
    {
        var user = await _userRepository.GetByUsernameOrEmailAsync(request.UsernameOrEmail);
        if (user is null)
        {
            return (null, "Invalid credentials.");
        }

        var passwordVerification = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (passwordVerification == PasswordVerificationResult.Failed)
        {
            return (null, "Invalid credentials.");
        }

        var token = _jwtTokenService.GenerateToken(user);

        var response = new UserLoginResponse
        {
            Token = token,
            User = user.ToContract()
        };

        return (response, null);
    }

    public async Task<UserResponse?> GetByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        var response = user?.ToContract();

        return user is not null ? response : null;
    }
    public async Task<UserResponse?> GetByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        var response = user?.ToContract();

        return user is not null ? response : null;
    }

    public async Task<IEnumerable<UserResponse>> GetUsers()
    {
        var users = await _userRepository.AllAsync() as List<User>;
        var response = new List<UserResponse>();
        users?.ForEach(u => response.Add(u.ToContract()));

        return response;
    }

    public async Task<(UserResponse? Response, string? Error)> UpdateUserAsync(Guid id, UserUpdateRequest request)
    {
        var user = await Repository.GetAsync(id);
        if (user is null)
        {
            return (null, "User was not found.");
        }

        if (!string.IsNullOrWhiteSpace(request.Username)
            && !string.Equals(user.Username, request.Username, StringComparison.Ordinal))
        {
            var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUsername is not null && existingUsername.Id != id)
            {
                return (null, "Username is already registered to another user.");
            }
        }

        if (!string.IsNullOrWhiteSpace(request.Email)
            && !string.Equals(user.Email, request.Email, StringComparison.Ordinal))
        {
            var existingEmail = await _userRepository.GetByEmailAsync(request.Email);
            if (existingEmail is not null && existingEmail.Id != id)
            {
                return (null, "Email is already registered to another user.");
            }
        }

        if (request.IsActive is null && user.IsActive)
        {
            request.IsActive = true;
        }

        if (request.IsAdmin is null && user.IsAdmin)
        {
            request.IsAdmin = true;
        }

        user = request.ToEntity();
        user.Id = id;
        Repository.Update(user);

        if (request.Password is not null)
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
        }

        await Repository.SaveChangesAsync();

        var response = user.ToContract();

        return (response, null);
    }
}