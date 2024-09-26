using App.Repositories;

namespace Repositories;
public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();

}
