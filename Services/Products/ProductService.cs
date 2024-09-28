using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Products;
using Services.Products.Create;
using Services.Products.Update;
using Services.Products.UpdateStock;
using System.Net;

namespace Services.Products;
public class ProductService(
    IProductRepository productRepository, 
    IUnitOfWork unitOfWork, 
    IMapper mapper) 
    : IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductsAsync(count);
        var productAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        var productAsDto = mapper.Map<List<ProductDto>>(products);

        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<List<ProductDto>>> GetPagedAllAsync(int pageNumber, int pageSize)
    {
        var products = await productRepository.GetAll().Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToListAsync();
        var productAsDto = mapper.Map<List<ProductDto>>(products);
       
        return ServiceResult<List<ProductDto>>.Success(productAsDto);
    }

    public async Task<ServiceResult<ProductDto>> GetByIdAsync(int id)
    {
        var product = await productRepository.GetById(id);

        if (product is null)
        {
            return ServiceResult<ProductDto>.Fail("Product Not Found", HttpStatusCode.NotFound);
        }

        var productAsDto = mapper.Map<ProductDto>(product);

        return ServiceResult<ProductDto>.Success(productAsDto!);
    }

    public async Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request)
    {

        //throw new CriticalException("kritik bir hata!!");

        var isProductNameExist = await productRepository.Where(x => x.Name == request.Name).AnyAsync();

        if (isProductNameExist)
        {
            return ServiceResult<CreateProductResponse>.Fail("Ürün adý veritabanýnda bulunmaktadýr.");
        }

        var product = mapper.Map<Product>(request);

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

        var isProductNameExist = await productRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();
 
        if (isProductNameExist)
        {
            return ServiceResult.Fail("Ürün adý veritabanýnda bulunmaktadýr.");
        }

        var product = mapper.Map<Product>(request);
        product.Id = id;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> UpdateProductStockAsync(UpdateProductStockRequest request)
    {
        var product = await productRepository.GetById(request.ProductId);

        product.Stock = request.Stock;

        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);

    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var product = await productRepository.GetById(id);

        productRepository.Delete(product!);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
