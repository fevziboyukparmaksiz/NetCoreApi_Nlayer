using System.Linq.Expressions;

namespace Repositories;
public interface IGenericRepository<T,TId> where T :BaseEntity<TId> where TId : struct
{
    IQueryable<T> GetAll();
    IQueryable<T> Where(Expression<Func<T,bool>> predicate);
    ValueTask<T?> GetById(TId id);
    ValueTask AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<bool> AnyAsync(TId id);

}
