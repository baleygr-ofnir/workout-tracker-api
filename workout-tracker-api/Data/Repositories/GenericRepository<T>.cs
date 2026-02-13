using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace workout_tracker_api.Data.Repositories;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly WorkoutContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(WorkoutContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }
    
    public virtual async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default) => await DbSet.FindAsync([id], cancellationToken);
    
    public virtual async Task<IReadOnlyList<T>> AllAsync(CancellationToken cancellationToken = default) => await DbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default) => await DbSet.Where(predicate).AsNoTracking().ToListAsync(cancellationToken);

    public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default) => await DbSet.AddAsync(entity, cancellationToken).AsTask();
    
    public virtual void Update(T entity) => DbSet.Update(entity);

    public virtual async Task<bool> Remove(Guid id, CancellationToken cancellationToken = default)
    {
        T? entity = await GetAsync(id, cancellationToken);
        if (entity == null) return false;
        DbSet.Remove(entity);
        return true;
    }
    
    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => await Context.SaveChangesAsync(cancellationToken);
}