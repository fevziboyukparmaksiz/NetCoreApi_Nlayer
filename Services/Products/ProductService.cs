using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Products;
using System.Net;

namespace Services.Products;
public class ProductService(
    IProductRepository productRepository, IUnitOfWork unitOfWork) 
    : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToListAsync();
        var productAsDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
       
        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetById(id);

        if (product is null)
        {
            return ServiceResult<ProductDto>.Fail("Product Not Found", HttpStatusCode.NotFound);
        }

        var productAsDto = new ProductDto(product!.Id, product.Name, product.Price, product.Stock);

        return ServiceResult<ProductDto>.Success(productAsDto!);
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {
        var product = new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateProductResponse>.SuccessAsCreated(
            new CreateProductResponse(product.Id),
            $"api/products/{product.Id}");
    }

    public async Task<ServiceResult> UpdateAsync(int id,UpdateProductRequest request)
    {
        //Fast Fail
        //Guard Clauses

        var product = await productRepository.GetById(id);

        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateProductStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetById(request.ProductId);

        if (product is null)
        {
            return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);

    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetById(id);

        if (product is null)
        {
           return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
        }

        productRepository.Delete(product!);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
