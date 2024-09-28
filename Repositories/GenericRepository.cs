using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositories;
public class GenericRepository<T,TId>(AppDbContext context) 
    : IGenericRepository<T,TId> where T:BaseEntity<TId> where TId : struct
{
    private readonly DbSet<T> _dbset = context.Set<T>();
    public IQueryable<T> GetAll() 
        => _dbset.AsQueryable().AsNoTracking();

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        => _dbset.Where(predicate).AsNoTracking();

    public async ValueTask<T?> GetById(TId id)
        => await _dbset.FindAsync(id);

    public async ValueTask AddAsync(T entity)
        => await _dbset.AddAsync(entity);

    public void Update(T entity)
        => _dbset.Update(entity);

    public void Delete(T entity)
        => _dbset.Remove(entity);

    public Task<bool> AnyAsync(TId id)
        => _dbset.AnyAsync(x => x.Id.Equals(id));
}
