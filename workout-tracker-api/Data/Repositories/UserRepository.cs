using workout_tracker_api.Data.Entities;

namespace workout_tracker_api.Data.Repositories;

public class UserRepository : GenericRepository<User>
{
    public UserRepository(WorkoutContext context) : base(context)
    {
    }

    public override void Update(User entity)
    {
        var user = DbSet.FirstOrDefault(u => u.Id == entity.Id);
        if (user is null) return;

        if (!string.IsNullOrEmpty(entity.Username)
            && !string.Equals(user.Username, entity.Username, StringComparison.Ordinal))
        {
            user.Username = entity.Username;
        }

        if (!string.IsNullOrEmpty(entity.Email)
            && !string.Equals(user.Email, entity.Email, StringComparison.Ordinal))
        {
            user.Email = entity.Email;
        }

        if (!string.IsNullOrEmpty(entity.PasswordHash)
            && !string.Equals(user.PasswordHash, entity.PasswordHash, StringComparison.Ordinal))
        {
            user.PasswordHash = entity.PasswordHash;
        }

        if (user.IsActive != entity.IsActive)
        {
            user.IsActive = entity.IsActive;
        }

        if (user.IsAdmin != entity.IsAdmin)
        {
            user.IsAdmin = entity.IsAdmin;
        }

        user.UpdatedAt = DateTime.UtcNow;

        base.Update(entity);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var user = await FindAsync(user => user.Username.ToLower() == username.ToLower());

        return user.FirstOrDefault();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        //if (string.IsNullOrWhiteSpace(email)) return null;

        var user = await FindAsync(u => u.Email.ToLower() == email.ToLower());

        return user.FirstOrDefault();
    }

    public async Task<User?> GetByUsernameOrEmailAsync(string usernameOrEmail)
    {
        var user = await FindAsync
        (
            user => user.Username.ToLower() == usernameOrEmail.ToLower()
                    || user.Email.ToLower() == usernameOrEmail.ToLower()
        );

        return user.FirstOrDefault();
    }
}