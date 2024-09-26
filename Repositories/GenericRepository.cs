using System.Linq.Expressions;
using App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repositories;
public class GenericRepository<T>(AppDbContext context) 
    : IGenericRepository<T> where T:class
{
    private readonly DbSet<T> _dbset = context.Set<T>();
    public IQueryable<T> GetAll() 
        => _dbset.AsQueryable().AsNoTracking();

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        => _dbset.Where(predicate).AsNoTracking();

    public async ValueTask<T?> GetById(int id)
        => await _dbset.FindAsync(id);

    public async ValueTask AddAsync(T entity)
        => await _dbset.AddAsync(entity);

    public void Update(T entity)
        => _dbset.Update(entity);

    public void Delete(T entity)
        => _dbset.Remove(entity);
}
