using Microsoft.EntityFrameworkCore;

namespace Repositories.Categories;
public class CategoryRepository(AppDbContext context) : GenericRepository<Category,int>(context),ICategoryRepository
{
    public Task<Category> GetCategoryWithProductsAsync(int id)
    {
        return context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
    }

    public IQueryable<Category> GetCategoryWithProducts()
    {
        return context.Categories.Include(c => c.Products).AsQueryable();
    }
}
