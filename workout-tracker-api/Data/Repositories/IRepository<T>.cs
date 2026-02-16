using System.Linq.Expressions;

namespace workout_tracker_api.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> AllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    IQueryable<T> AsQueryable();
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    Task<bool> Remove(Guid id, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}