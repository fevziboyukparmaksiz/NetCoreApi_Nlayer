using Services.Products.Create;
using Services.Products.Update;
using Services.Products.UpdateStock;

namespace Services.Products;
public interface IProductService 
{
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
    Task<ServiceResult<List<ProductDto>>> GetAllAsync();
    Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize);
    Task<ServiceResult<ProductDto>> GetByIdAsync(int id);
    Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateAsync(int id,UpdateProductRequest request);
    Task<ServiceResult> UpdateProductStockAsync(UpdateProductStockRequest request);
    Task<ServiceResult> DeleteAsync(int id);

}
