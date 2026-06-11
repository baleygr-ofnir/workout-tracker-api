using System.Linq.Expressions;
using workout_tracker_api.Data.Repositories;
using workout_tracker_api.Mapping;

namespace workout_tracker_api.Core.Services;

public abstract class GenericService<T> : IService<T> where T : class
{
    protected IRepository<T> Repository;

    public GenericService(IRepository<T> repository)
    {
        Repository = repository;
    }

    public async Task<T> AddAsync(T entity)
    {
        var added = await Repository.AddAsync(entity);
        await Repository.SaveChangesAsync();

        return added;
    }

    public async Task<T?> GetAsync(Guid id) => await Repository.GetAsync(id);

    public async Task<IEnumerable<T>> AllAsync() => await Repository.AllAsync();

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) => await Repository.FindAsync(predicate);

    public async Task<T?> Update(Guid id, T entity)
    {
        Repository.Update(entity);
        await Repository.SaveChangesAsync();

        var stored = await Repository.GetAsync(id);

        return stored;
    }

    public async Task<bool> Delete(Guid id)
    {
        var deleted = await Repository.Remove(id);
        if (deleted) await Repository.SaveChangesAsync();

        return deleted;
    }
}