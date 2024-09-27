using Microsoft.EntityFrameworkCore;

namespace Repositories.Products;
public class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
{
    public async Task<List<Product>> GetTopPriceProductsAsync(int count)
    {
        return await context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
    }
}
